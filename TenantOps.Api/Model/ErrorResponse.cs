namespace TenantOps.Api.Model;

public sealed record ErrorResponse(
       string Error,
       string Message);