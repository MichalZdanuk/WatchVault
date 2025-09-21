using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.AddMovie;

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
    }
}
