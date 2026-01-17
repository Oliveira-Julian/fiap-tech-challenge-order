using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface IAtualizaStatusPedidoUseCase
{
    Task<Pedido> ExecutarAsync(Guid idPedido, PedidoStatus pedidoStatus, CancellationToken cancellationToken);
}
