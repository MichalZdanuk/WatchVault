using System.Globalization;
using WatchVault.Domain.Entities;

namespace WatchVault.Application.Factories;
public sealed class WatchListAnalyticsFactory
    : IWatchListAnalyticsFactory
{
    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByDay(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate)
    {
        var allDates = Enumerable.Range(0, (endDate.Date - startDate.Date).Days + 1)
            .Select(d => startDate.Date.AddDays(d));

        var dict = items.GroupBy(x => x.WatchedAt!.Value.Date)
                        .ToDictionary(g => g.Key, g => new WatchListAnalyticsRecordDto(
                            g.Key.ToString("yyyy-MM-dd"),
                            g.Count(),
                            g.Sum(i => i.Movie.RuntimeMinutes ?? 0)));

        return allDates.Select(d => dict.TryGetValue(d, out var rec)
                ? rec
                : new WatchListAnalyticsRecordDto(d.ToString("yyyy-MM-dd"), 0, 0))
            .ToList();
    }

    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByWeek(
    IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate)
    {
        var current = startDate.Date;
        var end = endDate.Date;

        var dict = items.GroupBy(x => new
        {
            Year = ISOWeek.GetYear(x.WatchedAt!.Value),
            Week = ISOWeek.GetWeekOfYear(x.WatchedAt!.Value)
        })
        .ToDictionary(
            g => $"{g.Key.Year}-W{g.Key.Week:00}",
            g => new WatchListAnalyticsRecordDto(
                $"{g.Key.Year}-W{g.Key.Week:00}",
                g.Count(),
                g.Sum(i => i.Movie.RuntimeMinutes ?? 0))
        );

        var allWeeks = new List<string>();
        while (current <= end)
        {
            int year = ISOWeek.GetYear(current);
            int week = ISOWeek.GetWeekOfYear(current);
            string key = $"{year}-W{week:00}";

            if (!allWeeks.Contains(key))
            {
                allWeeks.Add(key);
            }

            current = current.AddDays(7);
        }

        return allWeeks
            .Select(w => dict.TryGetValue(w, out var rec)
                ? rec
                : new WatchListAnalyticsRecordDto(w, 0, 0))
            .ToList();
    }


    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByMonth(
        IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate)
    {
        var months = Enumerable.Range(0, ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month + 1)
            .Select(i => new DateTime(startDate.Year, startDate.Month, 1).AddMonths(i))
            .Select(d => $"{d.Year}-{d.Month:00}");

        var dict = items.GroupBy(x => new { x.WatchedAt!.Value.Year, x.WatchedAt!.Value.Month })
                        .ToDictionary(
                            g => $"{g.Key.Year}-{g.Key.Month:00}",
                            g => new WatchListAnalyticsRecordDto(
                                $"{g.Key.Year}-{g.Key.Month:00}",
                                g.Count(),
                                g.Sum(i => i.Movie.RuntimeMinutes ?? 0)));

        return months.Select(m => dict.TryGetValue(m, out var rec)
            ? rec
            : new WatchListAnalyticsRecordDto(m, 0, 0))
        .ToList();
    }

    public IReadOnlyList<WatchListAnalyticsRecordDto> AggregateByQuarter(
    IEnumerable<WatchListItem> items, DateTime startDate, DateTime endDate)
    {
        var dict = items
            .GroupBy(x => new
            {
                Year = x.WatchedAt!.Value.Year,
                Quarter = ((x.WatchedAt!.Value.Month - 1) / 3) + 1
            })
            .ToDictionary(
                g => $"Q{g.Key.Quarter} {g.Key.Year}",
                g => new WatchListAnalyticsRecordDto(
                    $"Q{g.Key.Quarter} {g.Key.Year}",
                    g.Count(),
                    g.Sum(i => i.Movie.RuntimeMinutes ?? 0))
            );

        var allQuarters = new List<string>();
        var current = GetQuarterStart(startDate);

        while (current <= endDate)
        {
            int quarter = ((current.Month - 1) / 3) + 1;
            allQuarters.Add($"Q{quarter} {current.Year}");
            current = current.AddMonths(3);
        }

        return allQuarters
            .Select(q => dict.TryGetValue(q, out var rec)
                ? rec
                : new WatchListAnalyticsRecordDto(q, 0, 0))
            .ToList();
    }

    private static DateTime GetQuarterStart(DateTime date)
    {
        int quarterStartMonth = date.Month switch
        {
            <= 3 => 1,
            <= 6 => 4,
            <= 9 => 7,
            _ => 10
        };
        return new DateTime(date.Year, quarterStartMonth, 1);
    }
}
