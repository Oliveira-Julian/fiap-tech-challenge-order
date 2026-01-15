using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Adapter.Gateways;

public class PreparoGateway(
    IKichensClient kichensClient) : IPreparoGateway
{
    public async Task<OrdemPedido> CadastrarAsync(OrdemPedido ordemPedido, CancellationToken cancellationToken)
    {
        var request = PreparoMapper.ToRequest(ordemPedido);

        var response = await kichensClient.CadastrarPreparoAsync(request, cancellationToken);

        if (response is null || !response.Sucesso)
            throw new Exception(Textos.ErroInesperado);

        var pagamento = PreparoMapper.ToDomain(response.Dados);

        return pagamento;
    }
}
