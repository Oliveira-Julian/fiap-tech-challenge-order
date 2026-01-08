using FoodChallenge.Infrastructure.Clients.MercadoPago.Clients;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago;

public static class BootstrapMercadoPago
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var settings = new MercadoPagoSettings(configuration);

        services.AddHttpClient<IMercadoPagoClient, MercadoPagoClient>(client =>
        {
            client.ConfigureBaseAddressForRestApi(settings.BaseUrl);
            client.ConfigureTimeout(settings.Timeout);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.AccessToken);
        })
          .AddHeaderPropagation();

        services.AddMemoryCache();

        services.AddSingleton<MercadoPagoSettings>();
    }
}
