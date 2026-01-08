using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PedidoPagamentoRepository(EntityFrameworkContext context)
    : RepositoryBase<PagamentoEntity>(context), IPedidoPagamentoRepository
{
    public async Task<PagamentoEntity> GetByIdMercadoPagoPagamentoAsync(string idMercadoPagoPagamento, CancellationToken cancellationToken, bool tracking = false)
    {
        return await GetQuery(tracking)
            .Where(entity => entity.IdMercadoPagoPagamento == idMercadoPagoPagamento)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
