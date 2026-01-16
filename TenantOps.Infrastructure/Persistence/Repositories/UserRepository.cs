using Microsoft.EntityFrameworkCore;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Identity;

namespace TenantOps.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly TenantOpsDbContext _context;

    public UserRepository(TenantOpsDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(Guid userId) =>
        _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

    public Task<User?> GetByEmailAsync( Email email) =>
        _context.Users.FirstOrDefaultAsync(x => x.Email.Value == email.Value);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
}
