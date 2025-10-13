using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json;
using System.Threading.RateLimiting;

namespace WatchVault.API;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(60);
                opt.PermitLimit = 60;
                opt.QueueLimit = 0;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            });

            options.AddFixedWindowLimiter("relaxed", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(60);
                opt.PermitLimit = 120;
                opt.QueueLimit = 0;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                var response = new
                {
                    StatusCode = StatusCodes.Status429TooManyRequests,
                    Message = "Rate limit exceeded. Please try again later."
                };

                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken: token);
            };
        });

        return services;
    }

    public static WebApplication UseRateLimiting(this WebApplication app)
    {
        app.UseRateLimiter();

        return app;
    }
}
