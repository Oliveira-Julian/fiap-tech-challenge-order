using FoodChallenge.Infrastructure.Http.Constants;
using System.Net;

namespace FoodChallenge.Infrastructure.Http.Extensions;

public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Realiza a deserialização do response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<T> MapearResponseAsync<T>(
        this HttpResponseMessage response,
        Func<string, string, Task> addValidationAsync = null,
        Action<string> error = null,
        Action onUnauthorized = null,
        CancellationToken cancellationToken = default)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsJsonAsync<T>(cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => await HandleUnauthorizedAsync(),
            HttpStatusCode.RequestTimeout => await HandleAndReturnAsync(CodigoValidacao.Timeout),
            _ => await HandleAndReturnAsync(CodigoValidacao.Generico)
        };

        async Task<T> HandleUnauthorizedAsync()
        {
            onUnauthorized?.Invoke();
            await HandleError(content, CodigoValidacao.CredenciaisInvalidas, addValidationAsync, error, response);
            return default;
        }

        async Task<T> HandleAndReturnAsync(string codigoValidacao)
        {
            await HandleError(content, codigoValidacao, addValidationAsync, error, response);
            return default;
        }
    }

    private static async Task HandleError(
        string rawResponse,
        string codigoValidacao,
        Func<string, string, Task> addValidationAsync,
        Action<string> error,
        HttpResponseMessage response)
    {
        error?.Invoke($"Erro HTTP {(int)response.StatusCode}: {rawResponse}");

        if (addValidationAsync is not null)
        {
            await addValidationAsync(codigoValidacao, "Ocorreu um erro no serviço, tente novamente mais tarde.");
        }
    }
}