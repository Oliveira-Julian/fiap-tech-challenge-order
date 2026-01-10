using FoodChallenge.Infrastructure.Clients.MercadoPago.Models;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Clients
{
    public interface IMercadoPagoClient
    {
        Task<OrderResponse> CadastrarOrdemAsync(Guid orderKey, CreateOrderRequest request, CancellationToken cancellationToken);
        Task<OrderResponse> ObterOrdemAsync(string id, CancellationToken cancellationToken);
    }
}
