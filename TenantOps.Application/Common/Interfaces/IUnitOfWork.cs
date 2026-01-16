namespace TenantOps.Application.Common.Interfaces;

// Coordinates persistence across repositories.
// Implementation lives in Infrastructure (EF Core).
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
