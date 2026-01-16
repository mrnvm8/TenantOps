using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Assets;
using TenantOps.Domain.Attendance;
using TenantOps.Domain.Identity;
using TenantOps.Domain.Organization;
using TenantOps.Domain.Tenants;

namespace TenantOps.Infrastructure.Persistence;

public sealed class TenantOpsDbContext : DbContext
{
    public TenantOpsDbContext(DbContextOptions<TenantOpsDbContext> options)
           : base(options) { }


    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<AssetAssignment> AssetAssignments => Set<AssetAssignment>();
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TenantOpsDbContext).Assembly);
    }

}
