using TenantOps.Domain.Common;
using TenantOps.Domain.Tenants;

namespace TenantOps.Domain.Contracts.Repositories;

public interface ITenantRepository : IRepository
{
    Task<Tenant?> GetByIdAsync(Guid tenantId);
    Task<Tenant?> GetByCodeAsync(string code);

    Task AddAsync(Tenant tenant);
    Task UpdateAsync(Tenant tenant);
}