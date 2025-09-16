using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Queries.SearchMovies;

namespace WatchVault.API.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var movies = app.MapGroup("/api/movies");

        movies.MapGet("/search", async ([FromQuery] string searchTerm, [FromQuery] int page, IMediator mediator) =>
        {
            var moviesList = await mediator.Send(new SearchMoviesQuery(searchTerm, page));
            return Results.Ok(moviesList);
        });
    }
}
