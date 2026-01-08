using Microsoft.Extensions.Configuration;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Settings;

public sealed class MercadoPagoSettings
{
    public MercadoPagoSettings(IConfiguration configuration)
    {
        configuration.Bind("MercadoPago", this);
    }

    public string BaseUrl { get; set; }
    public int Timeout { get; set; }
    public string AccessToken { get; set; }
    public string UserId { get; set; }
    public string CaixaCodigo { get; set; }
    public MercadoPagoPaths Paths { get; set; }

    public class MercadoPagoPaths
    {
        public string CadastrarOrdem { get; set; }
        public string ObterOrdem { get; set; }
        public string ObterPagamento { get; set; }
    }
}
