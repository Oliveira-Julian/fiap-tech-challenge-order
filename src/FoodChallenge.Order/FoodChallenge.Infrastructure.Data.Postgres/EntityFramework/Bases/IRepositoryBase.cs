using FoodChallenge.CrossCutting.Paging;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddItemsAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void UpdateItems(IEnumerable<TEntity> entity);
    void Remove(TEntity entity);
    void RemoveItems(IEnumerable<TEntity> entity);
    IQueryable<TEntity> GetQuery(bool tracking = false);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = false);
    Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid?> ids, CancellationToken cancellationToken, bool tracking = false);
    Task<Pagination<TEntity>> QueryPagedAsync<TFilter>(Filter<TFilter> filter, CancellationToken cancellationToken)
        where TFilter : class, new();
}
