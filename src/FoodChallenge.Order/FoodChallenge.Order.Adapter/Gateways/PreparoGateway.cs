using FoodChallenge.Infrastructure.Clients.Kitchens.Clients;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Adapter.Gateways;

public class PreparoGateway(
    IKitchensClient kitchensClient) : IPreparoGateway
{
    public async Task<OrdemPedido> CadastrarAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        var request = PreparoMapper.ToRequest(pedido);

        var response = await kitchensClient.CadastrarPreparoAsync(request, cancellationToken);

        if (response is null || !response.Sucesso)
            throw new Exception(Textos.ErroInesperado);

        var ordemPedido = PreparoMapper.ToDomain(response.Dados);

        return ordemPedido;
    }
}
