using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface IObtemPedidoUseCase
{
    Task<Pedido> ExecutarAsync(Guid idOrdemPedido, CancellationToken cancellationToken);
}
