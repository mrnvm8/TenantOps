using TenantOps.Domain.Common;
using TenantOps.Domain.Identity;

namespace TenantOps.Domain.Contracts.Repositories;

public interface IUserRepository : IRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(Email email);

    Task AddAsync(User user);
    Task UpdateAsync(User user);
}