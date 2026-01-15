using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface IConfirmarPagamentoUseCase
{
    Task<Pedido> ExecutarAsync(Guid idPedido, CancellationToken cancellationToken);
}
