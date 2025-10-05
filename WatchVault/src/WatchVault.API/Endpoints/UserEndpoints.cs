using MediatR;
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
        });
    }
}
