using FoodChallenge.Infrastructure.Clients.Kitchens.Clients;
using FoodChallenge.Infrastructure.Clients.Kitchens.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodChallenge.Infrastructure.Clients.Kitchens;

public static class BootstrapKitchens
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var settings = new KitchensSettings(configuration);

        services.AddHttpClient<IKitchensClient, KitchensClient>(client =>
        {
            client.ConfigureBaseAddressForRestApi(settings.BaseUrl);
            client.ConfigureTimeout(settings.Timeout);
        })
          .AddHeaderPropagation();

        services.AddHttpClient<IKitchenAuthenticationClient, KitchenAuthenticationClient>(client =>
        {
            client.ConfigureTimeout(settings.Timeout);
        })
          .AddHeaderPropagation();

        services.AddMemoryCache();

        services.AddSingleton<KitchensSettings>();
    }
}
