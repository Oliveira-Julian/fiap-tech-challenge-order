using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Application.Pagamentos;

public interface IPagamentoGateway
{
    Task<Pagamento> CadastrarPagamentoAsync(Pedido pedido, CancellationToken cancellationToken);
}
