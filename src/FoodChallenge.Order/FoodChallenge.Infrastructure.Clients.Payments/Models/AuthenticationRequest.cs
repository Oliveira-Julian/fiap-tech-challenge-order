namespace FoodChallenge.Infrastructure.Clients.Payments.Models;

public class AuthenticationRequest
{
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
}
