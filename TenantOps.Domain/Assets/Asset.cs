using TenantOps.Domain.Common;

namespace TenantOps.Domain.Assets;

// Aggregate Root representing an Asset owned by a Tenant.
// Controls the asset lifecycle and availability.
public sealed class Asset : AggregateRoot
{
    // Identifies the tenant that owns this asset.
    // Enforces multi-tenant boundaries.
    public Guid TenantId { get; private set; }

    // Human-readable name or identifier of the asset.
    public string Name { get; private set; }

    // Current lifecycle status of the asset (Available, Assigned, Retired).
    public AssetStatus Status { get; private set; }

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private Asset() { }

    // Public constructor enforces domain invariants at creation time.
    public Asset(Guid tenantId, string name)
    {
        // Business rule: asset must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: asset must have a name.
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Asset name is required.");

        TenantId = tenantId;
        Name = name.Trim();

        // Default lifecycle state for a newly created asset.
        Status = AssetStatus.Available;
    }

    // Marks the asset as assigned (e.g., issued to an employee).
    // Prevents invalid state transitions.
    public void Assign()
    {
        if (Status != AssetStatus.Available)
            throw new DomainException("Only available assets can be assigned.");

        Status = AssetStatus.Assigned;
    }

    // Retires the asset, removing it permanently from circulation.
    // Retired assets cannot be reassigned.
    public void Retire()
    {
        if (Status == AssetStatus.Retired)
            throw new DomainException("Asset is already retired.");

        Status = AssetStatus.Retired;
    }
}
