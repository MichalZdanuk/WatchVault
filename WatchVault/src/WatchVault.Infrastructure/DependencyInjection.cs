using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchVault.Application.Common;
using WatchVault.Application.Repositories;
using WatchVault.Infrastructure.Data;
using WatchVault.Infrastructure.Data.Repositories;
using WatchVault.Shared.Data.Interceptors;

namespace WatchVault.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddRepositories();
        services.AddCustomInterceptors();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<WatchVaultDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserContext, AuthenticatedUserContext>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
