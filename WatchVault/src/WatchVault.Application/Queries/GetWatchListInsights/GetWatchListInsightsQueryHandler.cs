using Microsoft.Extensions.Caching.Distributed;
using WatchVault.Application.Common;
using WatchVault.Application.Factories;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.GetWatchListInsights;
public sealed class GetWatchListInsightsQueryHandler(
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IWatchListInsightsFactory watchListInsightsFactory,
    IDistributedCache cache)
    : IQueryHandler<GetWatchListInsightsQuery, WatchListInsightsDto>
{
    public async Task<WatchListInsightsDto> Handle(GetWatchListInsightsQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var cacheKey = $"insights-{userId}";

        var result = await cache.GetOrSetAsync(cacheKey, async () =>
        {
            var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userId);
            if (watchList is null)
            {
                return new WatchListInsightsDto(0, 0, 0, 0,
                    new(), new(), new(), new());
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
        }, CacheProfiles.Analytics);

        return result ?? new WatchListInsightsDto(0, 0, 0, 0, new(), new(), new(), new());
    }
}
