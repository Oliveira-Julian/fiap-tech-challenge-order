using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Application.Preparos;

public interface IPreparoGateway
{
    Task<OrdemPedido> CadastrarAsync(Pedido pedido, CancellationToken cancellationToken);
}
