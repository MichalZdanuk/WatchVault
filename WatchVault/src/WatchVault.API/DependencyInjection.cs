using Azure.Storage.Blobs;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using WatchVault.API.Handlers;
using WatchVault.API.Options;

namespace WatchVault.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddCors(opt =>
        {
            var sp = services.BuildServiceProvider();
            var corsOptions = sp.GetRequiredService<IOptions<CorsPolicyOptions>>().Value;

            opt.AddPolicy(corsOptions.PolicyName, policyBuilder =>
            {
                policyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(corsOptions.AllowedOrigins);
            });
        });

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: configuration.GetConnectionString("Database")!,
                name: "postgresql",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "db", "postgres" }
            )
            .AddRedis(
                redisConnectionString: configuration.GetConnectionString("Redis")!,
                name: "redis",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "cache", "redis" }
            )
            .AddUrlGroup(
                new Uri(configuration["Keycloak:AuthServerUrl"]!),
                name: "keycloak",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "auth", "keycloak" }
            )
            .AddAzureBlobStorage(
                sp => new BlobServiceClient(configuration["AzureStorage:ConnectionString"]),
                name: "azurite",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "storage", "azurite" }
            );

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(10);
            options.AddHealthCheckEndpoint("API Health", "/health");
        })
        .AddInMemoryStorage();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        var corsOptions = app.Services.GetRequiredService<IOptions<CorsPolicyOptions>>().Value;

        app.UseExceptionHandler(options => { });
        app.UseCors(corsOptions.PolicyName);

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        app.MapHealthChecksUI(options =>
        {
            options.UIPath = "/health-ui";
        });

        return app;
    }
}
