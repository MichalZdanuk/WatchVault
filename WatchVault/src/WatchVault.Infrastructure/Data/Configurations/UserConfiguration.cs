using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchVault.Domain.Entities;
using WatchVault.Shared.Data;

namespace WatchVault.Infrastructure.Data.Configurations;
public class UserConfiguration : BaseEntityConfiguration<User>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);
    }
}
