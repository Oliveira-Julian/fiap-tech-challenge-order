using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;

public class ProdutoImagemRepository(EntityFrameworkContext context)
    : RepositoryBase<ProdutoImagemEntity>(context), IProdutoImagemRepository
{
    public async Task<ICollection<ProdutoImagemEntity>> GetByProductIdAsync(Guid idProduct, CancellationToken cancellationToken, bool tracking = false)
    {
        return await GetQuery(tracking)
            .Where(entity => entity.IdProduto == idProduct)
            .ToListAsync(cancellationToken);
    }
}
