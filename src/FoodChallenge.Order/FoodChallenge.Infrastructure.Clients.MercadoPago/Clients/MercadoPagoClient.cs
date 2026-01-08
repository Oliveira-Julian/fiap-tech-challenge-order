using FoodChallenge.Infrastructure.Clients.MercadoPago.Constants;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Models;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Settings;
using FoodChallenge.Infrastructure.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Clients
{
    internal sealed class MercadoPagoClient(
        ILogger<MercadoPagoClient> logger,
        HttpClient httpClient,
        MercadoPagoSettings mercadoPagoSettings) : IMercadoPagoClient
    {
        public async Task<OrderResponse> CadastrarOrdemAsync(Guid orderKey, CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogDebug(Logs.InicioExecucao, mercadoPagoSettings.Paths.CadastrarOrdem);

                var headers = new Dictionary<string, string>()
                {
                    {"X-Idempotency-Key", orderKey.ToString()},
                };

                var response = await httpClient.PostAsJsonAsync(mercadoPagoSettings.Paths.CadastrarOrdem, request, headers, cancellationToken);

                logger.LogDebug(Logs.FimExecucao, mercadoPagoSettings.Paths.CadastrarOrdem, response);

                return await response.MapearResponseAsync<OrderResponse>(
                    addValidationAsync: (codigo, mensagem) =>
                    {
                        logger.LogWarning(
                            Logs.ErroResponse,
                            mercadoPagoSettings.Paths.CadastrarOrdem,
                            (int)response.StatusCode,
                            mensagem
                        );
                        return Task.CompletedTask;
                    },
                    error: mensagem =>
                    {
                        logger.LogError(
                            Logs.ErroResponse,
                            mercadoPagoSettings.Paths.CadastrarOrdem,
                            (int)response.StatusCode,
                            mensagem
                        );
                    },
                    cancellationToken: cancellationToken
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Logs.ErroGenerico, mercadoPagoSettings.Paths.CadastrarOrdem);

                return default;
            }
        }

        public async Task<OrderResponse> ObterOrdemAsync(string id, CancellationToken cancellationToken)
        {
            var path = string.Format(mercadoPagoSettings.Paths.ObterOrdem, id);

            try
            {
                logger.LogDebug(Logs.InicioExecucao, path);

                var response = await httpClient.GetAsync(path, cancellationToken);

                logger.LogDebug(Logs.FimExecucao, path, response);

                return await response.MapearResponseAsync<OrderResponse>(
                    addValidationAsync: (codigo, mensagem) =>
                    {
                        logger.LogWarning(Logs.ErroResponse, path, (int)response.StatusCode, mensagem);
                        return Task.CompletedTask;
                    },
                    error: mensagem =>
                    {
                        logger.LogError(Logs.ErroResponse, path, (int)response.StatusCode, mensagem);
                    },
                    cancellationToken: cancellationToken
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Logs.ErroGenerico, path);
                return default;
            }
        }
    }
}
