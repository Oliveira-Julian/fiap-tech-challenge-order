using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface IFinalizaPedidoUseCase
{
    Task<Pedido> ExecutarAsync(Guid idPedido, CancellationToken cancellationToken);
}
