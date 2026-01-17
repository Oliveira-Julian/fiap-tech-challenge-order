using Microsoft.Extensions.Configuration;

namespace FoodChallenge.Infrastructure.Clients.Kitchens.Settings;

public sealed class KitchensSettings
{
    public KitchensSettings(IConfiguration configuration)
    {
        configuration.Bind("FoodChallengeKitchens", this);
    }

    public string BaseUrl { get; set; }
    public string AuthUrl { get; set; }
    public int Timeout { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public KitchensPaths Paths { get; set; }

    public class KitchensPaths
    {
        public string CadastrarOrdemPedido { get; set; }
    }
}
