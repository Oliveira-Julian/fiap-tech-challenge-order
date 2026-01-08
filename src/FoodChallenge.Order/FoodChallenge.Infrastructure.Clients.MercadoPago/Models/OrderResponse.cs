using System.Text.Json.Serialization;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Models;

public sealed class OrderResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("processing_mode")]
    public string ProcessingMode { get; set; }

    [JsonPropertyName("external_reference")]
    public string ExternalReference { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("total_amount")]
    public string TotalAmount { get; set; }

    [JsonPropertyName("expiration_time")]
    public string ExpirationTime { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("status_detail")]
    public string StatusDetail { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("last_updated_date")]
    public DateTime LastUpdatedDate { get; set; }

    [JsonPropertyName("integration_data")]
    public IntegrationData IntegrationData { get; set; }

    [JsonPropertyName("transactions")]
    public TransactionsResponse Transactions { get; set; }

    [JsonPropertyName("config")]
    public Config Config { get; set; }

    [JsonPropertyName("type_response")]
    public TypeResponse TypeResponse { get; set; }
}

public class IntegrationData
{
    [JsonPropertyName("application_id")]
    public string ApplicationId { get; set; }
}

public class TransactionsResponse
{
    [JsonPropertyName("payments")]
    public List<PaymentResponse> Payments { get; set; }
}

public class PaymentResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("amount")]
    public string Amount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("status_detail")]
    public string StatusDetail { get; set; }

    [JsonPropertyName("external_reference")]
    public string ExternalReference { get; set; }
}

public class TypeResponse
{
    [JsonPropertyName("qr_data")]
    public string QrData { get; set; }
}
