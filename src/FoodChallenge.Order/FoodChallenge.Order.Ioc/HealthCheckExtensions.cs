using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace FoodChallenge.Ioc;

public static class HealthCheckExtensions
{
    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
          .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"])
          .AddNpgSql(
              connectionString: builder.Configuration.GetConnectionString("DbConnection"),
              name: "DbConnection",
              failureStatus: HealthStatus.Unhealthy,
              tags: ["ready"]
        );

        return builder;
    }

    public static WebApplication MapHealthCheckDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("ready")
        });

        return app;
    }
}
