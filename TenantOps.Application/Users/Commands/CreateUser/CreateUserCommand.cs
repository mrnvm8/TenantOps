namespace TenantOps.Application.Users.Commands.CreateUser;

// Command represents intent to create a user.
// It contains ONLY the data required for the action.
public sealed record CreateUserCommand(string Email);
