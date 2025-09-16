using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.Login;
using WatchVault.Application.Commands.Register;
using WatchVault.Application.Queries.GetCurrentUser;

namespace WatchVault.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/auth");

        auth.MapPost("/register", async (RegisterCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });

        auth.MapPost("/login", async ([FromBody] LoginCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        auth.MapGet("/me", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCurrentUserQuery());
            return Results.Ok(result);
        });
    }
}
