using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface IPesquisaPedidoUseCase
{
    Task<Pagination<Pedido>> ExecutarAsync(Filter<PedidoFilter> filtro, CancellationToken cancellationToken);
}
