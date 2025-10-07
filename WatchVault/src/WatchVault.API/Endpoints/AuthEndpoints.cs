using MediatR;
using Microsoft.AspNetCore.Mvc;
using WatchVault.Application.Commands.Register;
using WatchVault.Application.DTO;
using WatchVault.Application.Queries.RetrieveToken;

namespace WatchVault.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/auth");

        auth.MapPost("/register", async ([FromForm] RegisterDto dto, IMediator mediator) =>
        {
            var command = new RegisterCommand(dto.FirstName,
                dto.LastName,
                dto.Username,
                dto.Email,
                dto.Password,
                dto.File);
            await mediator.Send(command);
            return Results.Created();
        })
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError)
        .DisableAntiforgery();

        auth.MapGet("/retrieve-token", async (string username, string password, IMediator mediator) =>
        {
            var command = new RetrieveToken(username, password);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .Produces<RetrieveTokenDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
