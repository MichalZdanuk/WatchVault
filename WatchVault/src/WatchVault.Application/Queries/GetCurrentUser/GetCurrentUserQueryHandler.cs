using WatchVault.Application.Common;
using WatchVault.Application.Repositories;
using WatchVault.Domain.Enums;

namespace WatchVault.Application.Queries.GetCurrentUser;
public sealed class GetCurrentUserQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetCurrentUserQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new UnauthorizedAccessException($"User {userId} not found.");
        }

        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userId);

        if (watchList is null)
        {
            throw new InvalidOperationException($"User {userContext.UserId} has no watchlist.");
        }

        var watchedMovies = watchList.Items
            .Where(i => i.WatchStatus == WatchStatus.Watched)
            .OrderByDescending(i => i.WatchedAt)
            .Take(10)
            .Select(i => new MovieSummaryDto(
                i.Movie.SimklId,
                i.Movie.Title,
                i.Movie.PosterUrl
            ))
            .ToList()
            ?? new List<MovieSummaryDto>();

        var toWatchMovies = watchList.Items
            .Where(i => i.WatchStatus == WatchStatus.ToWatch)
            .OrderByDescending(i => i.AddedToWatchAt)
            .Take(10)
            .Select(i => new MovieSummaryDto(
                i.Movie.SimklId,
                i.Movie.Title,
                i.Movie.PosterUrl
            ))
            .ToList()
            ?? new List<MovieSummaryDto>();

        var stats = new UserStatsDto(
            watchList.TotalWatched,
            watchList.TotalToWatch,
            watchList.TotalFavourites
        );

        return new UserProfileDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            stats,
            watchedMovies,
            toWatchMovies
        );
    }
}
