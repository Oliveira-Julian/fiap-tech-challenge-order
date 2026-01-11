namespace FoodChallenge.Order.Domain.Constants;

public static class AuthorizationPolicies
{
    public const string OrdersApi = "OrdersApi";
    public const string ConfigurationsApi = "ConfigurationsApi";
}

public static class AuthorizationScopes
{
    // Orders API Scopes
    public const string OrdersRead = "orders.read";
    public const string OrdersWrite = "orders.write";

    // Configurations API Scopes
    public const string ConfigurationsRead = "configurations.read";
    public const string ConfigurationsWrite = "configurations.write";
}

public static class Audiences
{
    public const string OrdersApi = "orders-api";
    public const string ConfigurationsApi = "configurations-api";
}
