using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WatchVault.Infrastructure.Data;
public static class Extensions
{
    public static void InitialiseDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WatchVaultDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
    }
}
