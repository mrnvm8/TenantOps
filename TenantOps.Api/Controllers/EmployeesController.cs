using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantOps.Application.Employees.Commands.CreateEmployee;
using TenantOps.Infrastructure.Security.Authorization;

namespace TenantOps.Api.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize(Policy = Policies.RequireManager)]
public class EmployeesController : ControllerBase
{
    private readonly CreateEmployeeHandler _handler;

    public EmployeesController(CreateEmployeeHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var employeeId =
            await _handler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = employeeId }, null);
    }
}
