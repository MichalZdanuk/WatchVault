using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.Login;
using WatchVault.Application.Commands.Register;
using WatchVault.Application.DTO;
using WatchVault.Application.Queries.GetCurrentUser;

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

        auth.MapPost("/login", async ([FromBody] LoginCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .Produces<LoginDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);

        auth.MapGet("/me", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCurrentUserQuery());
            return Results.Ok(result);
        })
        .Produces<UserDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
