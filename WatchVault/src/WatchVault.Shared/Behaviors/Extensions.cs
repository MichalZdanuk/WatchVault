using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WatchVault.Shared.Behaviors;
public static class ApplicationExtensions
{
    public static IServiceCollection AddMediatRWithBehaviors(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        return services;
    }
}
