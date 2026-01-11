using FoodChallenge.Infrastructure.Clients.Payments.Models;

namespace FoodChallenge.Infrastructure.Clients.Payments.Clients;

public interface IPaymentsClient
{
    Task<Resposta<PagamentoResponse>> CadastrarPagamentoAsync(CriarPagamentoRequest request, CancellationToken cancellationToken);
}
