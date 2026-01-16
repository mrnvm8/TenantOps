using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenantOps.Domain.Attendance;

namespace TenantOps.Infrastructure.Persistence.Configurations
{
    internal sealed class AttendanceRecordConfiguration
      : IEntityTypeConfiguration<AttendanceRecord>
    {
        public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            builder.HasIndex(x => new { x.TenantId, x.EmployeeId, x.Date })
                   .IsUnique();

            builder.Property(x => x.Status)
                   .HasConversion<int>()
                   .IsRequired();
        }
    }
}
