namespace TenantOps.Domain.Common;

// This class serves as a base for all entities in the domain.
public abstract class Entity
{
    public Guid Id { get; protected set; }
    protected Entity() => Id = Guid.NewGuid();
}

