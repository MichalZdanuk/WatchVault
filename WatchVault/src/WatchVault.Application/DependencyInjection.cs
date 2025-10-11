using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WatchVault.Application.Options;
using WatchVault.Application.Services;
using WatchVault.Shared.Behaviors;

namespace WatchVault.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();

        services.AddMediatRWithBehaviors(applicationAssembly);
        services.AddServices(configuration);
        services.AddDistributedRedisCache(configuration);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        services.Configure<SimklOptions>(configuration.GetSection("Simkl"));

        services.AddHttpClient<IUserRegistrationService, KeycloakService>();


        services.AddHttpClient<SimklApiConnector>();
        services.AddScoped<ISimklApiConnector>(sp =>
        {
            var inner = sp.GetRequiredService<SimklApiConnector>();
            var cache = sp.GetRequiredService<IDistributedCache>();
            return new SimklServiceCachingDecorator(inner, cache);
        });

        services.AddAzuriteBlobStorage(configuration);

        return services;
    }

    private static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnection = configuration.GetConnectionString("Redis") ?? "localhost:6379";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnection;
            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
            {
                AbortOnConnectFail = true,
                EndPoints = { redisConnection }
            };
        });

        return services;
    }

    private static IServiceCollection AddAzuriteBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBlobService, BlobService>();

        return services;
    }
}
