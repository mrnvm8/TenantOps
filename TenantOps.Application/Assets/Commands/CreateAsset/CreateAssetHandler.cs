using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Assets;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Application.Assets.Commands.CreateAsset;

public sealed class CreateAssetHandler
{
    private readonly IAssetRepository _assets;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAssetHandler(
        IAssetRepository assets,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _assets = assets;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateAssetCommand command,
        CancellationToken cancellationToken = default)
    {
        var asset = new Asset(
            _tenantProvider.TenantId,
            command.Name);

        await _assets.AddAsync(asset);
        await _unitOfWork.CommitAsync(cancellationToken);

        return asset.Id;
    }
}