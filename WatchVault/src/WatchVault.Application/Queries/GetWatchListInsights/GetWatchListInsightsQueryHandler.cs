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
                return new WatchListInsightsDto(0, 0, 0, 0.0, 0, 0,
                    new(), new(), new(), new(), new(), new("", "", 0, 0));
            }

            var items = watchList.Items;
            var watchedDayOfWeekDistribution = watchListInsightsFactory.WatchedDayOfWeekDistribution(items);
            var mostWatchedDayDto = PrepareMostWatchedDayDto(watchedDayOfWeekDistribution);

            return new WatchListInsightsDto(
                watchListInsightsFactory.TotalWatched(items),
                watchListInsightsFactory.TotalToWatch(items),
                watchListInsightsFactory.TotalFavorites(items),
                watchListInsightsFactory.FavoriteRate(items),
                watchListInsightsFactory.AverageRuntime(items),
                watchListInsightsFactory.TotalWatchedRuntimeMinutes(items),
                watchListInsightsFactory.WatchedGenresCount(items),
                watchListInsightsFactory.ToWatchGenresCount(items),
                watchListInsightsFactory.FavoriteGenresCount(items),
                watchListInsightsFactory.AverageRuntimePerGenre(items),
                watchedDayOfWeekDistribution,
                mostWatchedDayDto
            );
        }, CacheProfiles.Analytics);

        return result ?? new WatchListInsightsDto(0, 0, 0, 0.0, 0, 0,
                    new(), new(), new(), new(), new(), new("", "", 0, 0));
    }

    private static MostWatchedDayDto PrepareMostWatchedDayDto(Dictionary<string, int> watchedDayOfWeekDistribution)
    {
        if (watchedDayOfWeekDistribution.Count == 0)
        {
            return new MostWatchedDayDto("", "Movie Enthusiast", 0, 0);
        }

        var mostWatchedDay = watchedDayOfWeekDistribution
                        .OrderByDescending(x => x.Value)
                        .FirstOrDefault();
        var totalWatchedCount = watchedDayOfWeekDistribution.Sum(x => x.Value);
        var mostWatchedDayPercentage = totalWatchedCount == 0 ? 0 : (int)Math.Round((double)mostWatchedDay.Value / totalWatchedCount * 100);

        var mostWatchedDayDto = new MostWatchedDayDto(mostWatchedDay.Key, DetermineLabel(mostWatchedDay.Key), mostWatchedDay.Value, mostWatchedDayPercentage);
        return mostWatchedDayDto;
    }

    private static string DetermineLabel(string dayOfWeek)
    {
        return dayOfWeek switch
        {
            "Monday" => "Monday Motivator",
            "Tuesday" => "Tuesday Tuner",
            "Wednesday" => "Midweek Wanderer",
            "Thursday" => "Thursday Escapist",
            "Friday" => "Friday Night Flicker",
            "Saturday" => "Saturday Binger",
            "Sunday" => "Sunday Chiller",
            _ => "Movie Enthusiast"
        };
    }
}
