namespace TenantOps.Application.Common.Interfaces;

// Abstraction for issuing access tokens.
// Application does NOT know this is JWT.
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(
        Guid userId,
        Guid tenantId,
        string role);
}