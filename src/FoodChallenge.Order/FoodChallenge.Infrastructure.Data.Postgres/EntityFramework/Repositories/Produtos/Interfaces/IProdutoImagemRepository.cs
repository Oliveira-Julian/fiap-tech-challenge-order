using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;

public interface IProdutoImagemRepository : IRepositoryBase<ProdutoImagemEntity>
{
    Task<ICollection<ProdutoImagemEntity>> GetByProductIdAsync(Guid idProduct, CancellationToken cancellationToken, bool tracking = false);
}
