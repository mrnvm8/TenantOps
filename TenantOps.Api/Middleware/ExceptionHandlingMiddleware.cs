using System.Net;
using System.Text.Json;
using TenantOps.Api.Model;
using TenantOps.Domain.Common;

namespace TenantOps.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain exception occurred");
            await WriteErrorResponseAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            await WriteErrorResponseAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task WriteErrorResponseAsync(
        HttpContext context, 
        HttpStatusCode statusCode, 
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse(
               statusCode.ToString(),
               message);

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsJsonAsync(json);
    }
}

