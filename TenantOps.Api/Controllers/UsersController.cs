using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantOps.Application.Users.Commands.CreateUser;
using TenantOps.Infrastructure.Security.Authorization;

namespace TenantOps.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Policy = Policies.RequireAdmin)]
public sealed class UsersController : ControllerBase
{

    private readonly CreateUserHandler _handler;

    public UsersController(CreateUserHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var userId =
            await _handler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = userId }, null);
    }
}
