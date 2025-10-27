using WatchVault.Application.Common;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Repositories;
using WatchVault.Application.Services;
using WatchVault.Domain.Entities;

namespace WatchVault.Application.Commands.AddMovie;
public sealed class AddMovieCommandHandler(
        ISimklApiConnector simklApiConnector,
        IUnitOfWork unitOfWork,
        IUserContext userContext) : ICommandHandler<AddMovieCommand, Guid>
{
    public async Task<Guid> Handle(AddMovieCommand command, CancellationToken cancellationToken)
    {
        var movieDetails = await simklApiConnector.GetMovieDetailsAsync(command.SimklId);
        if (movieDetails is null)
        {
            throw new MovieNotFoundException(command.SimklId);
        }

        var movie = Domain.ValueObjects.Movie.Of(
            movieDetails.SimklId,
            movieDetails.Title,
            movieDetails.Year,
            movieDetails.Type,
            movieDetails.Poster,
            movieDetails.ReleaseDate,
            movieDetails.RuntimeMinutes,
            movieDetails.Director,
            movieDetails.Overview,
            movieDetails.Genres.ToList()
        );

        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);
        if (watchList is null)
        {
            throw new Exception($"User {userContext.UserId} has no watch list.");
        }

        WatchListItem item;

        _ = command.WatchStatus switch
        {
            Enums.WatchStatus.Watched => Do(() => watchList.AddWatched(movie)),
            Enums.WatchStatus.ToWatch => Do(() => watchList.AddToWatch(movie)),
            _ => throw new ArgumentException("Invalid watch status")
        };

        item = watchList.Items.Last();

        await unitOfWork.WatchListRepository.UpdateAsync(watchList);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return item.Id;
    }

    private static object? Do(Action action)
    {
        action();
        return null;
    }
}
