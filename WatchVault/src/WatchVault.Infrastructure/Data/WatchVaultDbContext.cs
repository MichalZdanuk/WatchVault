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
    public DbSet<WatchList> WatchLists { get; set; }
    public DbSet<WatchListItem> WatchListItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}