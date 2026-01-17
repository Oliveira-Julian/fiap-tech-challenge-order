using Microsoft.Extensions.Configuration;

namespace FoodChallenge.Infrastructure.Clients.Payments.Settings;

public sealed class PaymentsSettings
{
    public PaymentsSettings(IConfiguration configuration)
    {
        configuration.Bind("FoodChallengePayments", this);
    }

    public string BaseUrl { get; set; }
    public string AuthUrl { get; set; }
    public int Timeout { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public PaymentsPaths Paths { get; set; }

    public class PaymentsPaths
    {
        public string CadastrarPagamento { get; set; }
    }
}
