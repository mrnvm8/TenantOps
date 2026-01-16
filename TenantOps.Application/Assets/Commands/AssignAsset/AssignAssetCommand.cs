namespace TenantOps.Application.Assets.Commands.AssignAsset;

public sealed record AssignAssetCommand(
      Guid AssetId,
      Guid EmployeeId);