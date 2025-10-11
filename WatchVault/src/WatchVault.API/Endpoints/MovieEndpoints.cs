using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.DTO;
using WatchVault.Application.Enums;
using WatchVault.Application.Queries.GetMovieDetails;
using WatchVault.Application.Queries.GetTrendingMovies;
using WatchVault.Application.Queries.SearchMovies;

namespace WatchVault.API.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var simkl = app.MapGroup("/api/movies");

        simkl.MapGet("/search", async ([FromQuery] string searchTerm, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var movies = await mediator.Send(new SearchMoviesQuery(searchTerm, page, pageSize));
            return Results.Ok(movies);
        })
        .Produces<List<SearchMovieDto>>(StatusCodes.Status200OK);

        simkl.MapGet("/{id:int}", async (int id, IMediator mediator) =>
        {
            var details = await mediator.Send(new GetMovieDetailsQuery(id));
            return details is not null ? Results.Ok(details) : Results.NotFound();
        })
        .Produces<MovieDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        simkl.MapGet("/trending", async ([FromQuery] TrendingInterval trendingInterval, IMediator mediator) =>
        {
            var trending = await mediator.Send(new GetTrendingMoviesQuery(trendingInterval.ToString()));
            return Results.Ok(trending);
        })
        .Produces<List<TrendingMovieDto>>(StatusCodes.Status200OK);
    }
}
