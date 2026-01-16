using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Common;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Application.Assets.Commands.ReturnAsset;

public sealed class ReturnAssetHandler
{
    private readonly IAssetRepository _assets;
    private readonly IAssetAssignmentRepository _assignments;
    private readonly IUnitOfWork _unitOfWork;

    public ReturnAssetHandler(
        IAssetRepository assets,
        IAssetAssignmentRepository assignments,
        IUnitOfWork unitOfWork)
    {
        _assets = assets;
        _assignments = assignments;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        ReturnAssetCommand command,
        CancellationToken cancellationToken = default)
    {
        var assignment =
            await _assignments.GetByIdAsync(command.AssignmentId)
            ?? throw new DomainException("Assignment not found.");

        assignment.ReturnAsset();

        var asset =
            await _assets.GetByIdAsync(assignment.AssetId)
            ?? throw new DomainException("Asset not found.");

        asset.Assign(); // or asset.MarkAvailable() if you add it

        await _assignments.UpdateAsync(assignment);
        await _assets.UpdateAsync(asset);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}