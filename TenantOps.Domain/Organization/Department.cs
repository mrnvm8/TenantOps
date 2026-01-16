using TenantOps.Domain.Common;

namespace TenantOps.Domain.Organization;

// Represents a Department within a Tenant.
// This is an Entity, not an Aggregate Root, and is typically owned by a Tenant aggregate.
public sealed class Department : Entity
{
    // Identifies the tenant to which this department belongs.
    // Enforces multi-tenant boundaries.
    public Guid TenantId { get; private set; }

    // Human-readable name of the department.
    public string Name { get; private set; }

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private Department() { }

    // Public constructor enforces basic domain invariants.
    public Department(Guid tenantId, string name)
    {
        // Business rule: department must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: department must have a name.
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Department name is required.");

        TenantId = tenantId;
        Name = name.Trim();
    }
}
