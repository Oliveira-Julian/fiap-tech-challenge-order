using FoodChallenge.Common.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodChallenge.Ioc
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddEfPostgresDependency(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EntityFrameworkContext>(options => _ = options.UseNpgsql(connectionString));
            services.AddScoped<IUnitOfWork>(ctx => new UnitOfWork(ctx.GetRequiredService<EntityFrameworkContext>()));

            return services;
        }
    }
}
