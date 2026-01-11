using FoodChallenge.Infrastructure.Clients.Payments.Models;

namespace FoodChallenge.Infrastructure.Clients.Payments.Clients;

public interface IAuthenticationClient
{
    Task<TokenResponse> ObterTokenAsync(CancellationToken cancellationToken = default);
}
