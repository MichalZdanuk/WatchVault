using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using WatchVault.Shared.Exceptions;

namespace WatchVault.API.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred at {Time}", DateTime.UtcNow);

        (HttpStatusCode statusCode, string message) = exception switch
        {
            BadRequestException badRequestException => (HttpStatusCode.BadRequest, badRequestException.Message),
            NotFoundException notFoundException => (HttpStatusCode.NotFound, notFoundException.Message),
            ForbiddenException forbiddenException => (HttpStatusCode.Forbidden, forbiddenException.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);

        return true;
    }
}