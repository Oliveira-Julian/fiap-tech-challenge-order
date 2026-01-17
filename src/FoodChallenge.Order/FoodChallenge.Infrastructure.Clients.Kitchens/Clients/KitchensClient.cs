using FoodChallenge.Infrastructure.Clients.Kitchens.Constants;
using FoodChallenge.Infrastructure.Clients.Kitchens.Models;
using FoodChallenge.Infrastructure.Clients.Kitchens.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace FoodChallenge.Infrastructure.Clients.Kitchens.Clients;

internal sealed class KitchensClient(
        ILogger<KitchensClient> logger,
        HttpClient httpClient,
        IMemoryCache memoryCache,
        KitchensSettings KitchensSettings,
        IAuthenticationClient authenticationClient) : IKitchensClient
{
    public async Task<Resposta<OrdemPedidoResponse>> CadastrarPreparoAsync(CriarOrdemPedidoRequest request, CancellationToken cancellationToken)
    {
        logger.LogDebug(Logs.InicioExecucao, KitchensSettings.Paths.CadastrarOrdemPedido);

        try
        {
            if (!await AdicionarAutorizacaoAsync(cancellationToken))
                return default;

            var response = await httpClient.PostAsJsonAsync(KitchensSettings.Paths.CadastrarOrdemPedido, request, cancellationToken);

            logger.LogDebug(Logs.FimExecucao, KitchensSettings.Paths.CadastrarOrdemPedido, response);

            return await response.MapearResponseAsync<Resposta<OrdemPedidoResponse>>(
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
                        KitchensSettings.Paths.CadastrarOrdemPedido,
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
            logger.LogError(ex, Logs.ErroGenerico, KitchensSettings.Paths.CadastrarOrdemPedido);

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
