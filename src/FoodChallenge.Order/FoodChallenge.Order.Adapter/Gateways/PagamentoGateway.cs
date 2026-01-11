using FoodChallenge.Infrastructure.Clients.Payments.Clients;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Application.Pagamentos;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Adapter.Gateways;

public class PagamentoGateway(IPaymentsClient paymentsClient) : IPagamentoGateway
{
    public async Task<Pagamento> CadastrarPagamentoAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        var request = PagamentoMapper.ToRequest(pedido);

        var response = await paymentsClient.CadastrarPagamentoAsync(request, cancellationToken);

        if (response is null || !response.Sucesso)
            throw new Exception(Textos.ErroInesperado);

        var pagamento = PagamentoMapper.ToDomain(response.Dados);

        return pagamento;
    }
}
