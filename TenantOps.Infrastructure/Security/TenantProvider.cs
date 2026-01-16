using Microsoft.AspNetCore.Http;
using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Common;

namespace TenantOps.Infrastructure.Security;

// Resolves the current tenant from the HTTP context.
// This implementation assumes the TenantId is stored as a claim.
internal sealed class TenantProvider : ITenantProvider
{
    private const string TenantIdClaimType = "tenant_id";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext
                ?? throw new DomainException("No active HTTP context.");

            var claim = httpContext.User.FindFirst(TenantIdClaimType)
                ?? throw new DomainException("Tenant claim not found.");

            if (!Guid.TryParse(claim.Value, out var tenantId))
                throw new DomainException("Invalid TenantId claim.");

            return tenantId;
        }
    }
}