using TenantOps.Application.Common.Interfaces;
using TenantOps.Domain.Common;
using TenantOps.Domain.Common.Authorization;
using TenantOps.Domain.Contracts.Repositories;
using TenantOps.Domain.Identity;


namespace TenantOps.Application.Auth.Commands.Login;

// Application-level authentication orchestration
public sealed class LoginHandler
{
    private readonly IUserRepository _users;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginHandler(
        IUserRepository users,
        IJwtTokenGenerator tokenGenerator)
    {
        _users = users;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResult> HandleAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = new Email(command.Email);

        var user =
            await _users.GetByEmailAsync(email)
            ?? throw new DomainException("Invalid credentials.");

        if (!user.IsActive)
            throw new DomainException("User is inactive.");

        // NOTE:
        // Password verification would be delegated to Infrastructure
        // (hashing service), omitted here for clarity.

        var token = _tokenGenerator.GenerateAccessToken(
            user.Id,
            user.TenantId,
            RoleConstants.Employee); // role resolved elsewhere

        return new LoginResult(
            token,
            DateTime.UtcNow.AddHours(1));
    }
}