using WatchVault.Application.Enums;

namespace WatchVault.Application.Queries.GetWatchListAnalytics;
public record GetWatchListAnalyticsQuery(StatisticsPeriod Period, DateTime StartDate, DateTime EndDate) : IQuery<WatchListAnalyticsDto>;
