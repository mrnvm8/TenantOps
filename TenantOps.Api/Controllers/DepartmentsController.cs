using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantOps.Application.Departments.Commands.CreateDepartment;
using TenantOps.Infrastructure.Security.Authorization;

namespace TenantOps.Api.Controllers;

[ApiController]
[Route("api/departments")]
[Authorize(Policy = Policies.RequireAdmin)]
public sealed class DepartmentsController : ControllerBase
{

    private readonly CreateDepartmentHandler _handler;

    public DepartmentsController(CreateDepartmentHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentCommand command,
        CancellationToken cancellationToken)
    {
        await _handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
}
