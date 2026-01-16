using TenantOps.Domain.Common;
using TenantOps.Domain.Identity;
using TenantOps.Domain.Organization;

namespace TenantOps.Domain.Contracts.Repositories;

public interface IEmployeeRepository : IRepository
{
    Task<Employee?> GetByIdAsync(Guid employeeId);
    Task<Employee?> GetByEmailAsync(Guid tenantId, Email email);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
}
