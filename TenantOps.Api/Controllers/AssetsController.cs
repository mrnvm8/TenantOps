using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenantOps.Application.Assets.Commands.AssignAsset;
using TenantOps.Application.Assets.Commands.CreateAsset;
using TenantOps.Application.Assets.Commands.ReturnAsset;
using TenantOps.Infrastructure.Security.Authorization;

namespace TenantOps.Api.Controllers;

[ApiController]
[Route("api/assets")]
[Authorize(Policy = Policies.RequireManager)]
public sealed class AssetsController : ControllerBase
{
    private readonly CreateAssetHandler _createHandler;
    private readonly AssignAssetHandler _assignHandler;
    private readonly ReturnAssetHandler _returnHandler;

    public AssetsController(
        CreateAssetHandler createHandler,
        AssignAssetHandler assignHandler,
        ReturnAssetHandler returnHandler)
    {
        _createHandler = createHandler;
        _assignHandler = assignHandler;
        _returnHandler = returnHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAssetCommand command,
        CancellationToken cancellationToken)
    {
        var assetId =
            await _createHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = assetId }, null);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(
        [FromBody] AssignAssetCommand command,
        CancellationToken cancellationToken)
    {
        await _assignHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return(
        [FromBody] ReturnAssetCommand command,
        CancellationToken cancellationToken)
    {
        await _returnHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}
