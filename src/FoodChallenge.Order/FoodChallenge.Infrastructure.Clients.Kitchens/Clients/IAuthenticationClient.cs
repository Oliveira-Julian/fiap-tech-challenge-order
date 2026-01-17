using FoodChallenge.Infrastructure.Clients.Kitchens.Models;

namespace FoodChallenge.Infrastructure.Clients.Kitchens.Clients;

public interface IAuthenticationClient
{
    Task<TokenResponse> ObterTokenAsync(CancellationToken cancellationToken = default);
}
