using MediatR;
using WatchVault.Application.DTO;
using WatchVault.Application.Queries.GetCurrentUser;
using WatchVault.Application.Queries.GetUserAvatar;

namespace WatchVault.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var user = app.MapGroup("/api/user")
            .RequireAuthorization();

        user.MapGet("/avatar", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserAvatarQuery());

            return Results.File(result.Stream, result.ContentType);
        })
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        user.MapGet("/me", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetCurrentUserQuery());
            return Results.Ok(result);
        })
        .Produces<UserProfileDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
