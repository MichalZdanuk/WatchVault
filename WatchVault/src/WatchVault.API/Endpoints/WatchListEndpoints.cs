using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.AddMovie;
using WatchVault.Application.Commands.RemoveMovie;
using WatchVault.Application.Commands.ToggleFavourite;
using WatchVault.Application.DTO;
using WatchVault.Application.Enums;
using WatchVault.Application.Queries.BrowseWatchListItems;
using WatchVault.Application.Queries.GetWatchList;

namespace WatchVault.API.Endpoints;

public static class WatchListEndpoints
{
    public static void MapWatchListEndpoints(this WebApplication app)
    {
        var watchList = app.MapGroup("/api/watchlist")
            .RequireAuthorization()
            .RequireRateLimiting("relaxed");

        watchList.MapPost("/add", async ([FromBody] AddMovieCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/api/watchlist/items/{id}", new { Id = id });
        })
        .Produces<Guid>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Watchlist");

        watchList.MapGet("", async (IMediator mediator) =>
        {
            var watchListsummary = await mediator.Send(new GetWatchListSummaryQuery());

            return Results.Ok(watchListsummary);
        })
        .Produces<WatchListSummaryDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Watchlist");

        watchList.MapGet("/items", async ([FromQuery] Status? status, [FromQuery] int pageNumber, [FromQuery] int pageSize, IMediator mediator) =>
        {
            var result = await mediator.Send(new BrowseWatchListItemsQuery(status, pageNumber, pageSize));

            return Results.Ok(result);
        })
        .Produces<PagedWatchListItemsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Watchlist");

        watchList.MapDelete("/items/{id:Guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new RemoveWatchListItemCommand(id));

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Watchlist");

        watchList.MapPost("/items/{id:Guid}/favourite", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new ToggleFavouriteCommand(id));
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Watchlist");
    }
}
