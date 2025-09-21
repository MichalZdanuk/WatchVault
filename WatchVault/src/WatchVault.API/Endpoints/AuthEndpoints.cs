using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.Register;
using WatchVault.Application.DTO;
using WatchVault.Application.Queries.GetCurrentUser;
using WatchVault.Application.Queries.RetrieveToken;

namespace WatchVault.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/auth");

        auth.MapPost("/register", async ([FromBody] RegisterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        })
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        auth.MapGet("/retrieve-token", async (string username, string password, IMediator mediator) =>
        {
            var command = new RetrieveToken(username, password);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .Produces<RetrieveTokenDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        auth.MapGet("/me", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCurrentUserQuery());
            return Results.Ok(result);
        })
        .RequireAuthorization()
        .Produces<UserDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
