using TenantOps.Domain.Assets;
using TenantOps.Domain.Common;

namespace TenantOps.Domain.Contracts.Repositories;

public interface IAssetRepository : IRepository
{
    Task<Asset?> GetByIdAsync(Guid assetId);
    Task AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
}