using TenantOps.Domain.Common;

namespace TenantOps.Domain.Assets;

// Aggregate Root representing the assignment of an Asset to an Employee.
// This aggregate models the lifecycle of an asset assignment, including return.
public sealed class AssetAssignment : AggregateRoot
{
    // Identifies the tenant that owns this asset assignment.
    // Enforces multi-tenant isolation.
    public Guid TenantId { get; private set; }

    // Identifies the asset being assigned.
    // References the Asset aggregate by identity only.
    public Guid AssetId { get; private set; }

    // Identifies the employee to whom the asset is assigned.
    public Guid EmployeeId { get; private set; }

    // Timestamp when the asset was assigned.
    // Stored in UTC to avoid timezone issues.
    public DateTime AssignedAt { get; private set; }

    // Timestamp when the asset was returned.
    // Null indicates the asset is still assigned.
    public DateTime? ReturnedAt { get; private set; }

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private AssetAssignment() { }

    // Public constructor enforces creation-time invariants.
    public AssetAssignment(Guid tenantId, Guid assetId, Guid employeeId)
    {
        // Business rule: assignment must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: asset identifier must be valid.
        if (assetId == Guid.Empty)
            throw new DomainException("AssetId is required.");

        // Business rule: employee identifier must be valid.
        if (employeeId == Guid.Empty)
            throw new DomainException("EmployeeId is required.");

        TenantId = tenantId;
        AssetId = assetId;
        EmployeeId = employeeId;

        // Assignment time is captured at creation.
        AssignedAt = DateTime.UtcNow;
    }

    // Marks the asset as returned.
    // Prevents multiple returns for the same assignment.
    public void ReturnAsset()
    {
        // Business rule: asset cannot be returned more than once.
        if (ReturnedAt.HasValue)
            throw new DomainException("Asset has already been returned.");

        ReturnedAt = DateTime.UtcNow;
    }
}
