using Microsoft.AspNetCore.Authorization;
using TenantOps.Domain.Common.Authorization;

namespace TenantOps.Infrastructure.Security.Authorization;

public static class Policies
{
    public const string RequireAdmin = "RequireAdmin";
    public const string RequireManager = "RequireManager";
    public const string RequireEmployee = "RequireEmployee";

    public static void Register(AuthorizationOptions options)
    {
        options.AddPolicy(
            RequireAdmin,
            policy => policy.RequireRole(RoleConstants.Admin));

        options.AddPolicy(
            RequireManager,
            policy => policy.RequireRole(
                RoleConstants.Admin,
                RoleConstants.Manager));

        options.AddPolicy(
            RequireEmployee,
            policy => policy.RequireRole(
                RoleConstants.Admin,
                RoleConstants.Manager,
                RoleConstants.Employee));
    }
}