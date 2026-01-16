using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Identity;
using TenantOps.Domain.Organization;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly TenantOpsDbContext _context;

    public EmployeeRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByIdAsync(Guid employeeId)
    {
        return _context.Employees
            .FirstOrDefaultAsync(x => x.Id == employeeId);
    }

    public Task<Employee?> GetByEmailAsync(Guid tenantId, Email email)
    {
        return _context.Employees
            .FirstOrDefaultAsync(x =>
                x.TenantId == tenantId &&
                x.Email.Value == email.Value);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }
}