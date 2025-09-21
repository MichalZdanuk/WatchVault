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

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        services.Configure<SimklOptions>(configuration.GetSection("Simkl"));

        services.AddHttpClient<IMovieApiService, OmdbMovieApiService>();
        services.AddHttpClient<IUserRegistrationService, KeycloakService>();
        services.AddHttpClient<ISimklApiConnector, SimklApiConnector>();

        return services;
    }
}
