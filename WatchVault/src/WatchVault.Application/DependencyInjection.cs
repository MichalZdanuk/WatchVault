using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpClient<IMovieApiService, OmdbMovieApiService>();

        return services;
    }
}
