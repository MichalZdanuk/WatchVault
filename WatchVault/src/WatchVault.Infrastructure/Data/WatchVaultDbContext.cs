using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WatchVault.Domain.Entities;

namespace WatchVault.Infrastructure.Data;
public class WatchVaultDbContext
    : DbContext
{
    public WatchVaultDbContext(DbContextOptions<WatchVaultDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}