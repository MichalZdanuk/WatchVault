using WatchVault.Application.Common;
using WatchVault.Application.Repositories;
using WatchVault.Domain.Enums;

namespace WatchVault.Application.Queries.GetWatchListInsights;
public sealed class GetWatchListInsightsQueryHandler(
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetWatchListInsightsQuery, WatchListInsightsDto>
{
    public async Task<WatchListInsightsDto> Handle(GetWatchListInsightsQuery query, CancellationToken cancellationToken)
    {
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);
        if (watchList is null)
        {
            throw new InvalidOperationException($"User {userContext.UserId} has no watchlist.");
        }

        var items = watchList.Items;

        var totalWatched = items.Count(i => i.WatchStatus == WatchStatus.Watched);
        var totalToWatch = items.Count(i => i.WatchStatus == WatchStatus.ToWatch);
        var totalFavorites = items.Count(i => i.IsFavourite);
        var averageRuntime = (int)Math.Round(
            items
                .Where(i => i.WatchStatus == WatchStatus.Watched && i.Movie.RuntimeMinutes.HasValue)
                .Select(i => i.Movie.RuntimeMinutes!.Value)
                .DefaultIfEmpty(0)
                .Average()
        );

        var watchedGenresCount = items
            .Where(i => i.WatchStatus == WatchStatus.Watched)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

        var toWatchGenresCount = items
            .Where(i => i.WatchStatus == WatchStatus.ToWatch)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

        var favoriteGenresCount = items
            .Where(i => i.IsFavourite)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

        var averageRuntimePerGenre = items
            .Where(i => i.WatchStatus == WatchStatus.Watched && i.Movie.RuntimeMinutes.HasValue)
            .SelectMany(i => i.Movie.Genres.Select(g => new { Genre = g, Runtime = i.Movie.RuntimeMinutes!.Value }))
            .GroupBy(x => x.Genre)
            .ToDictionary(g => g.Key, g => (int)Math.Round(g.Average(x => x.Runtime)));

        return new WatchListInsightsDto(
            totalWatched,
            totalToWatch,
            totalFavorites,
            averageRuntime,
            watchedGenresCount,
            toWatchGenresCount,
            favoriteGenresCount,
            averageRuntimePerGenre
        );
    }
}
