using TenantOps.Domain.Common;

namespace TenantOps.Domain.Tenants;

public sealed class Tenant : AggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public TenantStatus Status { get; private set; }

    // Required by ORMs (e.g., EF Core) for materialization
    // Prevents direct instantiation outside the aggregate
    private Tenant() { }

    // Public constructor enforces domain invariants at creation time
    public Tenant(string name, string code)
    {
        // Business rule: tenant must have a name
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Tenant name is required.");

        Name = name;
        Code = code;
        // Default lifecycle state for new tenants
        Status = TenantStatus.Active;
    }

    // Suspends the tenant, preventing access or usage
    public void Suspend()
    {
        // Prevent invalid state transition
        if (Status == TenantStatus.Suspended)
            throw new DomainException("Tenant is already suspended.");

        Status = TenantStatus.Suspended;
    }

    // Reactivates a previously suspended tenant
    public void Activate()
    {
        // Prevent redundant transition
        if (Status == TenantStatus.Active)
            throw new DomainException("Tenant is already active.");

        Status = TenantStatus.Active;
    }
}
