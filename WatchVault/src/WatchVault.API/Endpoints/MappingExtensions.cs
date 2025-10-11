namespace WatchVault.API.Endpoints;

public static class MappingExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapTestEndpoints();
        app.MapMovieEndpoints();
        app.MapWatchListEndpoints();
        app.MapUserEndpoints();

        return app;
    }
}
