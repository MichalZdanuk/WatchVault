namespace WatchVault.API.Options;

public static class OptionsExtensions
{
    public static IServiceCollection AddApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CorsPolicyOptions>(configuration.GetSection("CorsPolicy"));

        return services;
    }
}
