using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Common;
using TenantOps.Domain.Contracts.Repositories;

namespace TenantOps.Application.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentHandler
{
    private readonly ITenantRepository _tenants;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentHandler(
        ITenantRepository tenants,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _tenants = tenants;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(
        CreateDepartmentCommand command,
        CancellationToken cancellationToken = default)
    {
        var tenant =
            await _tenants.GetByIdAsync(_tenantProvider.TenantId)
            ?? throw new DomainException("Tenant not found.");

        //tenant.AddDepartment(command.Name);

        await _tenants.UpdateAsync(tenant);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}