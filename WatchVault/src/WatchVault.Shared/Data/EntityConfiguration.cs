using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchVault.Shared.DDD;

namespace WatchVault.Shared.Data;
public class BaseEntityConfiguration<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreationDate)
            .IsRequired();

        builder.Property(e => e.UpdateDate)
                .IsRequired();
    }
}
