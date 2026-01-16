using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantOps.Application.Attendance.Commands.ClockIn;
using TenantOps.Infrastructure.Security.Authorization;

namespace TenantOps.Api.Controllers;

[ApiController]
[Route("api/attendance")]
[Authorize(Policy = Policies.RequireEmployee)]
public sealed class AttendanceController : ControllerBase
{
    private readonly ClockInHandler _clockInHandler;

    public AttendanceController(ClockInHandler clockInHandler)
    {
        _clockInHandler = clockInHandler;
    }

    [HttpPost("clock-in")]
    public async Task<IActionResult> ClockIn(
        [FromBody] ClockInCommand command,
        CancellationToken cancellationToken)
    {
        await _clockInHandler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}
