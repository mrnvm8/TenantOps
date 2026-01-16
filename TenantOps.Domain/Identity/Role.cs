using TenantOps.Domain.Common;

namespace TenantOps.Domain.Identity;

public sealed class Role : Entity
{
    public string Name { get; private set; }

    // Required by EF Core
    private Role() { }

    internal Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Role name is required.");

        Name = name.Trim();
    }
}
