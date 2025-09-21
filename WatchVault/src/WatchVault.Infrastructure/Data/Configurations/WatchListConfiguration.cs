using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchVault.Domain.Entities;
using WatchVault.Shared.Data;

namespace WatchVault.Infrastructure.Data.Configurations;
public class WatchListConfiguration
    : BaseEntityConfiguration<WatchList>, IEntityTypeConfiguration<WatchList>
{
    public override void Configure(EntityTypeBuilder<WatchList> builder)
    {
        base.Configure(builder);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<WatchList>(x => x.UserId);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(i => i.WatchListId);
    }
}
