using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly EntityFrameworkContext _context = null;
        private readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(EntityFrameworkContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task AddItemsAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateItems(IEnumerable<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveItems(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public virtual IQueryable<TEntity> GetQuery(bool tracking = false)
        {
            if (tracking)
            {
                return _dbSet.AsQueryable();
            }

            return _dbSet.AsQueryable().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
        {
            return await GetQuery(tracking)
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid?> ids, CancellationToken cancellationToken, bool tracking = false)
        {
            return await GetQuery(tracking)
                .Where(entity => ids.Contains(entity.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<Pagination<TEntity>> QueryPagedAsync<TFilter>(Filter<TFilter> filter, CancellationToken cancellationToken)
            where TFilter : class, new()
        {
            var query = GetQuery();

            query = ApplyFilters(query, filter.Fields);

            if (!string.IsNullOrEmpty(filter.FieldOrdenation))
                query = ApplyOrdering(query, filter.FieldOrdenation, filter.OrdenationAsc);

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((filter.Page - 1) * filter.SizePage)
                .Take(filter.SizePage)
                .ToListAsync(cancellationToken);

            return new Pagination<TEntity>(filter.Page, items.Count, total, items);
        }

        protected static IQueryable<TEntity> ApplyFilters<TFilter>(IQueryable<TEntity> query, TFilter filterFields)
            where TFilter : class
        {
            if (filterFields == null)
                return query;

            var predicate = PredicateBuilder.New<TEntity>(true);
            var parameter = Expression.Parameter(typeof(TEntity), "e");

            foreach (var filterProp in typeof(TFilter).GetProperties())
            {
                if (filterProp.GetCustomAttribute<IgnoreFilterAttribute>() != null)
                    continue;

                var value = filterProp.GetValue(filterFields);
                if (value == null || value.Equals(GetDefault(value.GetType())))
                    continue;

                var filterAttr = filterProp.GetCustomAttribute<FilterByAttribute>();
                var entityPropName = filterAttr?.PropertyName ?? filterProp.Name;
                var filterType = filterAttr?.FilterType ?? FilterType.Equals;

                var entityProp = typeof(TEntity).GetProperty(entityPropName);
                if (entityProp == null)
                    continue;

                var left = Expression.Property(parameter, entityProp);

                if (TryBuildCollectionPredicate(filterProp, value, left, parameter, out var collectionLambda))
                {
                    predicate = predicate.And(collectionLambda);
                    continue;
                }

                var right = Expression.Constant(value, entityProp.PropertyType);
                var condition = BuildConditionExpression(left, right, filterType, entityProp.PropertyType);
                var lambdaFinal = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);
                predicate = predicate.And(lambdaFinal);
            }

            return query.AsExpandable().Where(predicate);
        }

        private static Expression BuildConditionExpression(
            MemberExpression left,
            ConstantExpression right,
            FilterType filterType,
            Type entityPropType)
        {
            return filterType switch
            {
                FilterType.Contains when entityPropType == typeof(string)
                    => Expression.Call(left, nameof(string.Contains), null, right),
                FilterType.GreaterThan => Expression.GreaterThan(left, right),
                FilterType.LessThan => Expression.LessThan(left, right),
                FilterType.GreaterOrEqual => Expression.GreaterThanOrEqual(left, right),
                FilterType.LessOrEqual => Expression.LessThanOrEqual(left, right),
                FilterType.NotEqual => Expression.NotEqual(left, right),
                _ => Expression.Equal(left, right)
            };
        }

        private static bool TryBuildCollectionPredicate(
            PropertyInfo filterProp,
            object value,
            MemberExpression left,
            ParameterExpression parameter,
            out Expression<Func<TEntity, bool>> lambda)
        {
            lambda = default!;

            if (filterProp.PropertyType == typeof(string)) return false;
            if (!typeof(System.Collections.IEnumerable).IsAssignableFrom(filterProp.PropertyType)) return false;
            if (!filterProp.PropertyType.IsGenericType) return false;

            var genericArgs = filterProp.PropertyType.GetGenericArguments();
            if (genericArgs.Length == 0) return false;

            var genericType = genericArgs[0];

            var containsMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                .MakeGenericMethod(genericType);

            var listConstant = Expression.Constant(value);
            var containsCall = Expression.Call(containsMethod, listConstant, left);

            lambda = Expression.Lambda<Func<TEntity, bool>>(containsCall, parameter);
            return true;
        }

        private static IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, string field, bool asc)
        {
            if (string.IsNullOrEmpty(field))
                return query.OrderByDescending(e => e.DataCriacao);

            var propertyInfo = typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => string.Equals(p.Name, field, StringComparison.OrdinalIgnoreCase));

            if (propertyInfo == null)
                return query.OrderByDescending(e => e.DataCriacao);

            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.PropertyOrField(parameter, field);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = asc ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable)
                .GetMethods()
                .Single(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), property.Type);

            return (IQueryable<TEntity>)method.Invoke(null, [query, lambda]);
        }

        private static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
