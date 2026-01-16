using TenantOps.Domain.Common;

namespace TenantOps.Domain.Identity;

// Aggregate Root representing a system user.
// Controls identity, lifecycle, and role assignments.
public sealed class User : AggregateRoot
{
    // Identifies the tenant this user belongs to.
    // This enforces multi-tenancy boundaries.
    public Guid TenantId { get; private set; }

    // Email address represented as a Value Object.
    // Immutable and validated at creation time.
    public Email Email { get; private set; }

    // Indicates whether the user account is active.
    // Lifecycle state is controlled via Activate/Deactivate methods.
    public bool IsActive { get; private set; }

    // Collection of role identifiers assigned to the user.
    // HashSet prevents duplicate role assignments.
    // Only role IDs are stored to avoid cross-aggregate references.
    private readonly HashSet<Guid> _roleIds = new();

    // Read-only exposure of assigned roles.
    // Prevents external modification of the collection.
    public IReadOnlyCollection<Guid> RoleIds => _roleIds;

    // Required by EF Core for materialization.
    // Prevents uncontrolled creation outside the aggregate.
    private User() { }

    // Public constructor enforces domain invariants.
    public User(Guid tenantId, Email email)
    {
        // Business rule: user must belong to a tenant.
        if (tenantId == Guid.Empty)
            throw new DomainException("TenantId is required.");

        // Business rule: email address is mandatory.
        Email = email ?? throw new DomainException("Email is required.");

        TenantId = tenantId;

        // Default lifecycle state for a newly created user.
        IsActive = true;
    }

    // Assigns a role to the user by role identity.
    // Enforces uniqueness and prevents invalid assignments.
    public void AssignRole(Guid roleId)
    {
        // Business rule: role ID must be valid.
        if (roleId == Guid.Empty)
            throw new DomainException("RoleId is required.");

        // HashSet.Add returns false if the role already exists.
        if (!_roleIds.Add(roleId))
            throw new DomainException("Role already assigned.");
    }

    // Deactivates the user account.
    // Prevents redundant or invalid state transitions.
    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("User is already inactive.");

        IsActive = false;
    }

    // Reactivates a previously deactivated user account.
    // Prevents redundant or invalid state transitions.
    public void Activate()
    {
        if (IsActive)
            throw new DomainException("User is already active.");

        IsActive = true;
    }
}
