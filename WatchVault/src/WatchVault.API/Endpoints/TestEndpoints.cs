namespace WatchVault.API.Endpoints;

public static class TestEndpoints
{
    public static void MapTestEndpoints(this WebApplication app)
    {
        var test = app.MapGroup("/api/test")
            .RequireRateLimiting("relaxed");

        test.MapGet("/ping", () => Results.Ok("pong"))
            .Produces<string>(StatusCodes.Status200OK)
            .WithTags("Test")
            .ExcludeFromDescription();
    }
}
