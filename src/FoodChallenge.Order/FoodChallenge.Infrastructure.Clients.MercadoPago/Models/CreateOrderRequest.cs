using System.Text.Json.Serialization;

namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Models;

public sealed class CreateOrderRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("total_amount")]
    public string TotalAmount { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("external_reference")]
    public string ExternalReference { get; set; }

    [JsonPropertyName("expiration_time")]
    public string ExpirationTime { get; set; }

    [JsonPropertyName("config")]
    public Config Config { get; set; }

    [JsonPropertyName("transactions")]
    public Transactions Transactions { get; set; }

    [JsonPropertyName("items")]
    public IEnumerable<Item> Items { get; set; }
}

public class Config
{
    [JsonPropertyName("qr")]
    public Qr Qr { get; set; }
}

public class Qr
{
    [JsonPropertyName("external_pos_id")]
    public string ExternalPosId { get; set; }

    [JsonPropertyName("mode")]
    public string Mode { get; set; }
}

public class Transactions
{
    [JsonPropertyName("payments")]
    public IEnumerable<Payment> Payments { get; set; }
}

public class Payment
{
    [JsonPropertyName("amount")]
    public string Amount { get; set; }
}

public class Item
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("unit_price")]
    public string UnitPrice { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unit_measure")]
    public string UnitMeasure { get; set; }

    [JsonPropertyName("external_code")]
    public string ExternalCode { get; set; }

    [JsonPropertyName("external_categories")]
    public IEnumerable<ExternalCategory> ExternalCategories { get; set; }
}

public class ExternalCategory
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
