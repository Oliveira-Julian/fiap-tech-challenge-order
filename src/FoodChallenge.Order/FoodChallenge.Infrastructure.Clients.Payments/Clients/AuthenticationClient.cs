using FoodChallenge.Infrastructure.Clients.Payments.Constants;
using FoodChallenge.Infrastructure.Clients.Payments.Models;
using FoodChallenge.Infrastructure.Clients.Payments.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace FoodChallenge.Infrastructure.Clients.Payments.Clients;

public class AuthenticationClient(
    HttpClient httpClient,
    PaymentsSettings paymentsSettings,
    ILogger<AuthenticationClient> logger) : IAuthenticationClient
{
    public async Task<TokenResponse> ObterTokenAsync(CancellationToken cancellationToken = default)
    {
        logger.LogDebug(Logs.InicioExecucao, paymentsSettings.AuthUrl);

        try
        {
            var response = await httpClient.SendAsync(CriarHttpRequestMessage(), cancellationToken);

            logger.LogDebug(Logs.FimExecucao, paymentsSettings.AuthUrl, response);

            return await response.MapearResponseAsync<TokenResponse>(
                addValidationAsync: (codigo, mensagem) =>
                {
                    logger.LogWarning(
                        Logs.ErroResponse,
                        paymentsSettings.AuthUrl,
                        (int)response.StatusCode,
                        mensagem
                    );
                    return Task.CompletedTask;
                },
                error: mensagem =>
                {
                    logger.LogError(
                        Logs.ErroResponse,
                        paymentsSettings.AuthUrl,
                        (int)response.StatusCode,
                        mensagem
                    );
                },
                cancellationToken: cancellationToken
            );
        }
        catch (Exception ex) when (ex is TimeoutException || ex.InnerException is TimeoutException)
        {
            logger.LogError(ex, Logs.ErroGenerico, paymentsSettings.AuthUrl);

            return default;
        }
    }

    private HttpRequestMessage CriarHttpRequestMessage()
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", paymentsSettings.ClientId },
            { "client_secret", paymentsSettings.ClientSecret },
            { "scope", paymentsSettings.Scope }
        });

        return new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(paymentsSettings.AuthUrl),
            Content = content
        };
    }
}
