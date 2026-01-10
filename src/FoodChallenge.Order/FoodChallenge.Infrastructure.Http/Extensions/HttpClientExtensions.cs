using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FoodChallenge.Infrastructure.Http.Extensions;

public static class HttpClientExtensions
{
    public static void ConfigureBaseAddressForRestApi(this HttpClient httpClient, string baseAddress)
    {
        httpClient.BaseAddress = new Uri(baseAddress);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static void ConfigureTimeout(this HttpClient httpClient, int timeout)
    {
        httpClient.Timeout = TimeSpan.FromSeconds(timeout);
    }


    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data, CancellationToken cancellationToken)
    {
        var content = GetHttpContent(data);

        return httpClient.PostAsync(url, content, cancellationToken);
    }

    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data, IDictionary<string, string> headers, CancellationToken cancellationToken)
    {
        var content = GetHttpContent(data);

        foreach (var header in headers)
        {
            content.Headers.Add(header.Key, header.Value);
        }

        return httpClient.PostAsync(url, content, cancellationToken);
    }

    private static StringContent GetHttpContent<T>(T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

        return content;
    }
}
