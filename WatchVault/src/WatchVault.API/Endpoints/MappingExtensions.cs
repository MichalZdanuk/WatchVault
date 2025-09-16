namespace WatchVault.API.Endpoints;

public static class MappingExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapMovieEndpoints();
        app.MapTestEndpoints();

        return app;
    }
}
