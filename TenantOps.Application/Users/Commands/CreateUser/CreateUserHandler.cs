using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Common;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Identity;

namespace TenantOps.Application.Users.Commands.CreateUser;

// Handles the CreateUser use case.
// Orchestrates domain logic and persistence.
public sealed class CreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantProvider _tenantProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(
        IUserRepository userRepository,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tenantProvider = tenantProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken = default)
    {
        // Create domain value object
        var email = new Email(command.Email);

        // Optional uniqueness check
        var existingUser =
            await _userRepository.GetByEmailAsync(email);

        if (existingUser is not null)
            throw new DomainException("User with this email already exists.");

        // Create aggregate
        var user = new User(
            _tenantProvider.TenantId,
            email);

        // Persist
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync(cancellationToken);

        return user.Id;
    }
}
