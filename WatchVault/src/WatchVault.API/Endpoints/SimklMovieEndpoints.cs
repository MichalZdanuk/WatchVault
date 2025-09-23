using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.DTO;
using WatchVault.Application.Enums;
using WatchVault.Application.Queries.SimkGetlTrendingMovies;
using WatchVault.Application.Queries.SimklGetMovieDetails;
using WatchVault.Application.Queries.SimklSearchMovies;

namespace WatchVault.API.Endpoints;

public static class SimklMovieEndpoints
{
    public static void MapSimklMovieEndpoints(this WebApplication app)
    {
        var simkl = app.MapGroup("/api/simkl/movies");

        simkl.MapGet("/search", async ([FromQuery] string searchTerm, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var movies = await mediator.Send(new SimklSearchMoviesQuery(searchTerm, page, pageSize));
            return Results.Ok(movies);
        })
        .Produces<List<SimklSearchMovieDto>>(StatusCodes.Status200OK);

        simkl.MapGet("/{id:int}", async (int id, IMediator mediator) =>
        {
            var details = await mediator.Send(new SimklGetMovieDetailsQuery(id));
            return details is not null ? Results.Ok(details) : Results.NotFound();
        })
        .Produces<SimklMovieDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        simkl.MapGet("/trending", async ([FromQuery] TrendingInterval trendingInterval, IMediator mediator) =>
        {
            var trending = await mediator.Send(new SimklGetTrendingMoviesQuery(trendingInterval.ToString()));
            return Results.Ok(trending);
        })
        .Produces<List<SimklTrendingMovieDto>>(StatusCodes.Status200OK);
    }
}
