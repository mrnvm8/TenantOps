using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantOps.Domain.Identity;

namespace TenantOps.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                 .HasColumnName("Email")
                 .IsRequired();
        });

        builder.HasIndex(x => new { x.TenantId })
               .HasDatabaseName("IX_User_Tenant");

    }
}
