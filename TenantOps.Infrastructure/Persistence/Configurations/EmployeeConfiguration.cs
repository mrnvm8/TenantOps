using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantOps.Domain.Organization;

namespace TenantOps.Infrastructure.Persistence.Configurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                 .HasColumnName("Email")
                 .IsRequired();
        });

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();
    }
}
