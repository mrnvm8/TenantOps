namespace TenantOps.Application.Auth.Commands.Login;

public sealed record LoginResult(
       string AccessToken,
       DateTime ExpiresAt);