namespace TenantOps.Application.Employees.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(
       Guid DepartmentId,
       string FirstName,
       string LastName,
       string Email);
