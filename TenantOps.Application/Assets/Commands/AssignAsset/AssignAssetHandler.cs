using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Assets;
using TenantOps.Domain.Common;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Application.Assets.Commands.AssignAsset;

public sealed class AssignAssetHandler
{
    private readonly IAssetRepository _assets;
    private readonly IAssetAssignmentRepository _assignments;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AssignAssetHandler(
        IAssetRepository assets,
        IAssetAssignmentRepository assignments,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _assets = assets;
        _assignments = assignments;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        AssignAssetCommand command,
        CancellationToken cancellationToken = default)
    {
        var asset =
            await _assets.GetByIdAsync(command.AssetId)
            ?? throw new DomainException("Asset not found.");

        var activeAssignment =
            await _assignments.GetActiveByAssetIdAsync(command.AssetId);

        if (activeAssignment is not null)
            throw new DomainException("Asset is already assigned.");

        asset.Assign();

        var assignment = new AssetAssignment(
            _tenantProvider.TenantId,
            command.AssetId,
            command.EmployeeId);

        await _assignments.AddAsync(assignment);
        await _assets.UpdateAsync(asset);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}