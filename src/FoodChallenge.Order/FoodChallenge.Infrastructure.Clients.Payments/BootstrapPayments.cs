using FoodChallenge.Infrastructure.Clients.Payments.Clients;
using FoodChallenge.Infrastructure.Clients.Payments.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodChallenge.Infrastructure.Clients.Payments;

public static class BootstrapPayments
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var settings = new PaymentsSettings(configuration);

        services.AddHttpClient<IPaymentsClient, PaymentsClient>(client =>
        {
            client.ConfigureBaseAddressForRestApi(settings.BaseUrl);
            client.ConfigureTimeout(settings.Timeout);
        })
          .AddHeaderPropagation();

        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>(client =>
        {
            client.ConfigureTimeout(settings.Timeout);
        })
          .AddHeaderPropagation();

        services.AddMemoryCache();

        services.AddSingleton<PaymentsSettings>();
    }
}
