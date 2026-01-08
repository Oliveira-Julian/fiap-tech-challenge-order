using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos.Interfaces;

public interface ICadastraPedidoUseCase
{
    Task<Pedido> ExecutarAsync(string cpf, IEnumerable<PedidoItem> itens, CancellationToken cancellationToken);
}
