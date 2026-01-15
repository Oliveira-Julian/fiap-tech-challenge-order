using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Application.Preparos;

public interface IPreparoGateway
{
    Task<OrdemPedido> CadastrarAsync(OrdemPedido ordemPedido, CancellationToken cancellationToken);
}
