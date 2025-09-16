using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WatchVault.Infrastructure.Data;
public class WatchVaultDbContext
    : DbContext
{
    private const string DbSchema = "watchVault";

    public WatchVaultDbContext(DbContextOptions<WatchVaultDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(DbSchema);

        base.OnModelCreating(modelBuilder);
    }
}