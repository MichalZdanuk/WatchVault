using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.AddMovie;
using WatchVault.Application.DTO;
using WatchVault.Application.Queries.GetWatchList;

namespace WatchVault.API.Endpoints;

public static class WatchListEndpoints
{
    public static void MapWatchListEndpoints(this WebApplication app)
    {
        var watchList = app.MapGroup("/api/watchlist").RequireAuthorization();

        watchList.MapPost("/add", async ([FromBody] AddMovieCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/watchlist/items/{id}", new { Id = id });
        })
        .Produces<Guid>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        watchList.MapGet("", async (IMediator mediator) =>
        {
            var watchList = await mediator.Send(new GetWatchListQuery());

            return Results.Ok(watchList);
        })
        .Produces<WatchListDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
