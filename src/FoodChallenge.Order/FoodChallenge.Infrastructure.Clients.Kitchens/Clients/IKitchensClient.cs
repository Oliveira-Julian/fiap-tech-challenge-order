using FoodChallenge.Infrastructure.Clients.Kitchens.Models;

namespace FoodChallenge.Infrastructure.Clients.Kitchens.Clients;

public interface IKitchensClient
{
    Task<Resposta<OrdemPedidoResponse>> CadastrarPreparoAsync(CadastrarOrdemPedidoRequest request, CancellationToken cancellationToken);
}
