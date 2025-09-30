using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchVault.Domain.Entities;
using WatchVault.Shared.Data;

namespace WatchVault.Infrastructure.Data.Configurations;
public class WatchListItemConfiguration
    : BaseEntityConfiguration<WatchListItem>, IEntityTypeConfiguration<WatchListItem>
{
    public override void Configure(EntityTypeBuilder<WatchListItem> builder)
    {
        base.Configure(builder);

        builder.ComplexProperty(x => x.Movie, movieBuilder =>
        {
            movieBuilder.Property(x => x.SimklId).IsRequired();
            movieBuilder.Property(x => x.Title).IsRequired();
            movieBuilder.Property(x => x.Year).IsRequired();
            movieBuilder.Property(x => x.PosterUrl).IsRequired();
            movieBuilder.Property(x => x.ReleaseDate).IsRequired(false);
            movieBuilder.Property(x => x.RuntimeMinutes).IsRequired(false);
            movieBuilder.Property(x => x.Director).IsRequired();
            movieBuilder.Property(x => x.Overview).IsRequired();
        });
    }
}
