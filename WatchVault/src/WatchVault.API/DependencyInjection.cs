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

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        var corsOptions = app.Services.GetRequiredService<IOptions<CorsPolicyOptions>>().Value;

        app.UseExceptionHandler(options => { });
        app.UseCors(corsOptions.PolicyName);

        return app;
    }
}
