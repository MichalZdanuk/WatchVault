using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
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
            movieBuilder.Property(x => x.PosterUrl).IsRequired(false);
            movieBuilder.Property(x => x.ReleaseDate).IsRequired(false);
            movieBuilder.Property(x => x.RuntimeMinutes).IsRequired(false);
            movieBuilder.Property(x => x.Director).IsRequired(false);
            movieBuilder.Property(x => x.Overview).IsRequired();
            movieBuilder.Property(x => x.Genres)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                .HasColumnType("jsonb")
                .IsRequired(false);
        });
    }
}
