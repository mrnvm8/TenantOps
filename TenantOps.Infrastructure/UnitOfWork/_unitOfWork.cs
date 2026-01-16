using TenantOps.Application.Common.Interfaces;
using TenantOps.Infrastructure.Persistence;

namespace TenantOps.Infrastructure.UnitOfWork;

internal sealed class _unitOfWork : IUnitOfWork
{
    private readonly TenantOpsDbContext _context;

    public _unitOfWork(TenantOpsDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}