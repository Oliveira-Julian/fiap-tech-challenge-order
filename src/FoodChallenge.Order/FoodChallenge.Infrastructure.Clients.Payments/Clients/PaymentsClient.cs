using FoodChallenge.Infrastructure.Clients.Payments.Constants;
using FoodChallenge.Infrastructure.Clients.Payments.Models;
using FoodChallenge.Infrastructure.Clients.Payments.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace FoodChallenge.Infrastructure.Clients.Payments.Clients;

internal sealed class PaymentsClient(
        ILogger<PaymentsClient> logger,
        HttpClient httpClient,
        IMemoryCache memoryCache,
        PaymentsSettings paymentsSettings,
        IAuthenticationClient authenticationClient) : IPaymentsClient
{
    public async Task<Resposta<PagamentoResponse>> CadastrarPagamentoAsync(CriarPagamentoRequest request, CancellationToken cancellationToken)
    {
        logger.LogDebug(Logs.InicioExecucao, paymentsSettings.Paths.CadastrarPagamento);

        try
        {
            if (!await AdicionarAutorizacaoAsync(cancellationToken))
                return default;

            var response = await httpClient.PostAsJsonAsync(paymentsSettings.Paths.CadastrarPagamento, request, cancellationToken);

            logger.LogDebug(Logs.FimExecucao, paymentsSettings.Paths.CadastrarPagamento, response);

            return await response.MapearResponseAsync<Resposta<PagamentoResponse>>(
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
                        paymentsSettings.Paths.CadastrarPagamento,
                        (int)response.StatusCode,
                        mensagem
                    );
                },
                onUnauthorized: () => memoryCache.Remove(CacheAutorizacaoToken.TokenAutorizacao),
                cancellationToken: cancellationToken
            );
        }
        catch (Exception ex) when (ex is TimeoutException || ex.InnerException is TimeoutException)
        {
            logger.LogError(ex, Logs.ErroGenerico, paymentsSettings.Paths.CadastrarPagamento);

            return default;
        }
    }

    private async Task<bool> AdicionarAutorizacaoAsync(CancellationToken cancellationToken)
    {
        var tokenResponse = await memoryCache.GetOrCreateAsync(CacheAutorizacaoToken.TokenAutorizacao, async (entry) =>
        {
            var tokenResponse = await authenticationClient.ObterTokenAsync(cancellationToken);

            if (tokenResponse is null)
            {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow;
                return default;
            }

            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(tokenResponse.ExpiresIn);

            return tokenResponse;
        });

        if (tokenResponse?.AccessToken is null)
            return false;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenResponse.TokenType, tokenResponse.AccessToken);

        return true;
    }
}
