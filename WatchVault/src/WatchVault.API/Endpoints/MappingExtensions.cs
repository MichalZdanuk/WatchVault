namespace WatchVault.API.Endpoints;

public static class MappingExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapMovieEndpoints();
        app.MapTestEndpoints();
        app.MapSimklMovieEndpoints();
        app.MapWatchListEndpoints();

        return app;
    }
}
