using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Assets;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class AssetAssignmentRepository : IAssetAssignmentRepository
{
    private readonly TenantOpsDbContext _context;

    public AssetAssignmentRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<AssetAssignment?> GetByIdAsync(Guid assignmentId)
    {
        return _context.AssetAssignments
            .FirstOrDefaultAsync(x => x.Id == assignmentId);
    }

    public Task<AssetAssignment?> GetActiveByAssetIdAsync(Guid assetId)
    {
        return _context.AssetAssignments
            .FirstOrDefaultAsync(x =>
                x.AssetId == assetId &&
                x.ReturnedAt == null);
    }

    public async Task AddAsync(AssetAssignment assignment)
    {
        await _context.AssetAssignments.AddAsync(assignment);
    }

    public Task UpdateAsync(AssetAssignment assignment)
    {
        _context.AssetAssignments.Update(assignment);
        return Task.CompletedTask;
    }
}
