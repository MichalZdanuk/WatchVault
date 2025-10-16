using WatchVault.Domain.Entities;

namespace WatchVault.Application.Factories;
public interface IWatchListAnalyticsFactory
{
    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByDay(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate);
    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByWeek(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate);
    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByMonth(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate);
    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByQuarter(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate);
}
