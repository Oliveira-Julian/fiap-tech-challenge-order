using FoodChallenge.Infrastructure.Clients.Kitchens.Constants;
using FoodChallenge.Infrastructure.Clients.Kitchens.Models;
using FoodChallenge.Infrastructure.Clients.Kitchens.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace FoodChallenge.Infrastructure.Clients.Kitchens.Clients;

public class AuthenticationClient(
    HttpClient httpClient,
    KitchensSettings KitchensSettings,
    ILogger<AuthenticationClient> logger) : IAuthenticationClient
{
    public async Task<TokenResponse> ObterTokenAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug(Logs.InicioExecucao, KitchensSettings.AuthUrl);

        try
        {
            var response = await httpClient.SendAsync(CriarHttpRequestMessage(), cancellationToken);

            logger.LogDebug(Logs.FimExecucao, KitchensSettings.AuthUrl, response);

            return await response.MapearResponseAsync<TokenResponse>(
                addValidationAsync: (codigo, mensagem) =>
                {
                    logger.LogWarning(
                        Logs.ErroResponse,
                        KitchensSettings.AuthUrl,
                        (int)response.StatusCode,
                        mensagem
                    );
                    return Task.CompletedTask;
                },
                error: mensagem =>
                {
                    logger.LogError(
                        Logs.ErroResponse,
                        KitchensSettings.AuthUrl,
                        (int)response.StatusCode,
                        mensagem
                    );
                },
                cancellationToken: cancellationToken
            );
        }
        catch (Exception ex) when (ex is TimeoutException || ex.InnerException is TimeoutException)
        {
            logger.LogError(ex, Logs.ErroGenerico, KitchensSettings.AuthUrl);

            return default;
        }
    }

    private HttpRequestMessage CriarHttpRequestMessage()
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", KitchensSettings.ClientId },
            { "client_secret", KitchensSettings.ClientSecret }
        });

        return new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(KitchensSettings.AuthUrl),
            Content = content
        };
    }
}
