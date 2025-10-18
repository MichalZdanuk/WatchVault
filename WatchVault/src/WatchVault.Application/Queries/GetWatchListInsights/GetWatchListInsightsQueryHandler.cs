using WatchVault.Application.Common;
using WatchVault.Application.Factories;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.GetWatchListInsights;
public sealed class GetWatchListInsightsQueryHandler(
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IWatchListInsightsFactory watchListInsightsFactory)
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

        return new WatchListInsightsDto(
            watchListInsightsFactory.TotalWatched(items),
            watchListInsightsFactory.TotalToWatch(items),
            watchListInsightsFactory.TotalFavorites(items),
            watchListInsightsFactory.AverageRuntime(items),
            watchListInsightsFactory.WatchedGenresCount(items),
            watchListInsightsFactory.ToWatchGenresCount(items),
            watchListInsightsFactory.FavoriteGenresCount(items),
            watchListInsightsFactory.AverageRuntimePerGenre(items)
        );
    }
}
