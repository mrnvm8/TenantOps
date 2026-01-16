using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Assets;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class AssetRepository : IAssetRepository
{
    private readonly TenantOpsDbContext _context;

    public AssetRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<Asset?> GetByIdAsync(Guid assetId)
    {
        return _context.Assets
            .FirstOrDefaultAsync(x => x.Id == assetId);
    }

    public async Task AddAsync(Asset asset)
    {
        await _context.Assets.AddAsync(asset);
    }

    public Task UpdateAsync(Asset asset)
    {
        _context.Assets.Update(asset);
        return Task.CompletedTask;
    }
}