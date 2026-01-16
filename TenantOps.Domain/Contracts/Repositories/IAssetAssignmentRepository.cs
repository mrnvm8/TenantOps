using TenantOps.Domain.Assets;
using TenantOps.Domain.Common;

namespace TenantOps.Domain.Contracts.Repositories;

public interface IAssetAssignmentRepository : IRepository
{
    Task<AssetAssignment?> GetByIdAsync(Guid assignmentId);
    Task<AssetAssignment?> GetActiveByAssetIdAsync(Guid assetId);
    Task AddAsync(AssetAssignment assignment);
    Task UpdateAsync(AssetAssignment assignment);
}