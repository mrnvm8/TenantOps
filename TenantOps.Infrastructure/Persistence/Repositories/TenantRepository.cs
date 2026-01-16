using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Tenants;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class TenantRepository : ITenantRepository
{
    private readonly TenantOpsDbContext _context;

    public TenantRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<Tenant?> GetByIdAsync(Guid tenantId)
    {
        return _context.Tenants
            .FirstOrDefaultAsync(x => x.Id == tenantId);
    }

    public Task<Tenant?> GetByCodeAsync(string code)
    {
        return _context.Tenants
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task AddAsync(Tenant tenant)
    {
        await _context.Tenants.AddAsync(tenant);
    }

    public Task UpdateAsync(Tenant tenant)
    {
        _context.Tenants.Update(tenant);
        return Task.CompletedTask;
    }
}