using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantOps.Domain.Assets;

namespace TenantOps.Infrastructure.Persistence.Configurations;

internal sealed class AssetAssignmentConfiguration
        : IEntityTypeConfiguration<AssetAssignment>
{
    public void Configure(EntityTypeBuilder<AssetAssignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.AssetId).IsRequired();
        builder.Property(x => x.EmployeeId).IsRequired();
        builder.Property(x => x.AssignedAt).IsRequired();

        builder.HasIndex(x => x.AssetId)
               //.HasFilter("[ReturnedAt] IS NULL")
               .IsUnique();
    }
}
