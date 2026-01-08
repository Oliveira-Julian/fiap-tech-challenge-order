using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PedidoRepository(EntityFrameworkContext context)
    : RepositoryBase<PedidoEntity>(context), IPedidoRepository
{
    public async Task<PedidoEntity> GetByIdWithRelationsAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
    {
        return await GetQuery(tracking)
            .Include(p => p.Cliente)
            .Include(p => p.Pagamento)
            .Include(p => p.Itens)
            .ThenInclude(p => p.Produto)
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<PedidoEntity>> ObterPedidosPorProdutoAsync(Guid idProduto,
        IEnumerable<int> pedidosStatus,
        CancellationToken cancellationToken,
        bool tracking = false)
    {
        return await GetQuery(tracking)
            .Include(p => p.Itens)
            .Where(p => pedidosStatus.Contains(p.Status) && p.Itens.Any(i => i.IdProduto == idProduto))
            .ToListAsync(cancellationToken);
    }
}
