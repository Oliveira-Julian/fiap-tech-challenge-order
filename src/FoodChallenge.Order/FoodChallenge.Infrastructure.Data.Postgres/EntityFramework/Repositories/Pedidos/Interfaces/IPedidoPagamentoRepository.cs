using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;

public interface IPedidoPagamentoRepository : IRepositoryBase<PagamentoEntity>
{
    Task<PagamentoEntity> GetByIdMercadoPagoPagamentoAsync(string idMercadoPagoPagamento, CancellationToken cancellationToken, bool tracking = false);
}
