using System.Text.Json;

namespace FoodChallenge.Infrastructure.Http.Extensions;

public static class HttpContentExtensions
{
    /// <summary>
    /// Realiza a deserialização do response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content, CancellationToken cancellationToken)
    {
        var dataAsString = await content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(dataAsString);
    }
}
