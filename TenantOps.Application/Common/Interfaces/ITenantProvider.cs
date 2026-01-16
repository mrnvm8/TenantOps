namespace TenantOps.Application.Common.Interfaces;

// Provides access to the current tenant context.
// Implemented in Infrastructure (e.g., HTTP context, JWT).
public interface ITenantProvider
{
    Guid TenantId { get; }
}