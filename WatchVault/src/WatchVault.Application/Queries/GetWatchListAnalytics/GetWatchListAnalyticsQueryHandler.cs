using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Factories;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.GetWatchListAnalytics;
public sealed class GetWatchListAnalyticsQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,
    IWatchListAnalyticsFactory watchListAnalyticsFactory)
    : IQueryHandler<GetWatchListAnalyticsQuery, WatchListAnalyticsDto>
{
    public async Task<WatchListAnalyticsDto> Handle(GetWatchListAnalyticsQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var items = await unitOfWork.WatchListRepository.GetWatchedItemsInRangeAsync(userId, query.StartDate, query.EndDate);

        if (!items.Any())
        {
            return new WatchListAnalyticsDto(query.Period.ToString(), new List<WatchListAnalyticsRecordDto>());
        }

        IReadOnlyList<WatchListAnalyticsRecordDto> groupedData = query.Period switch
        {
            StatisticsPeriod.Day => watchListAnalyticsFactory.AggregateByDay(items, query.StartDate, query.EndDate),
            StatisticsPeriod.Week => watchListAnalyticsFactory.AggregateByWeek(items, query.StartDate, query.EndDate),
            StatisticsPeriod.Month => watchListAnalyticsFactory.AggregateByMonth(items, query.StartDate, query.EndDate),
            StatisticsPeriod.Quarter => watchListAnalyticsFactory.AggregateByQuarter(items, query.StartDate, query.EndDate),
            _ => throw new ArgumentException($"Invalid period: {query.Period}")
        };

        return new WatchListAnalyticsDto(query.Period.ToString(), groupedData);
    }
}
