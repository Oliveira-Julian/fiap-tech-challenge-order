using FoodChallenge.Order.Domain.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodChallenge.Ioc;

public static class AuthorizationExtensions
{
    public static void AddOpenIdDictValidation(this IServiceCollection services, IConfiguration configuration)
    {
        var authority = configuration["OAuth:Authority"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.RequireHttpsMetadata = false;
                options.MetadataAddress = $"{authority}.well-known/openid-configuration";

                // Aceitar certificados auto-assinados em desenvolvimento
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authority,
                    ValidateAudience = true,
                    ValidAudiences = [Audiences.OrdersApi, Audiences.ConfigurationsApi],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.OrdersApi, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("aud", Audiences.OrdersApi);
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "scope" &&
                        (c.Value.Contains(AuthorizationScopes.OrdersRead) ||
                         c.Value.Contains(AuthorizationScopes.OrdersWrite))));
            });

            options.AddPolicy(AuthorizationPolicies.ConfigurationsApi, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("aud", Audiences.ConfigurationsApi);
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "scope" &&
                        (c.Value.Contains(AuthorizationScopes.ConfigurationsRead) ||
                         c.Value.Contains(AuthorizationScopes.ConfigurationsWrite))));
            });
        });
    }
}

