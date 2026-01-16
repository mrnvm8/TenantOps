using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Identity;
using TenantOps.Domain.Organization;

namespace TenantOps.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeHandler(
        IEmployeeRepository employeeRepository,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = new Email(command.Email);

        var employee = new Employee(
            _tenantProvider.TenantId,
            command.DepartmentId,
            command.FirstName,
            command.LastName,
            email);

        await _employeeRepository.AddAsync(employee);
        await _unitOfWork.CommitAsync(cancellationToken);

        return employee.Id;
    }
}
