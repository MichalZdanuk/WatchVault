using WatchVault.Domain.Entities;
using WatchVault.Domain.Enums;

namespace WatchVault.Application.Factories;
public class WatchListInsightsFactory()
    : IWatchListInsightsFactory
{
    public int TotalWatched(IEnumerable<WatchListItem> items) =>
        items.Count(i => i.WatchStatus == WatchStatus.Watched);

    public int TotalToWatch(IEnumerable<WatchListItem> items) =>
        items.Count(i => i.WatchStatus == WatchStatus.ToWatch);

    public int TotalFavorites(IEnumerable<WatchListItem> items) =>
        items.Count(i => i.IsFavourite);

    public int AverageRuntime(IEnumerable<WatchListItem> items) =>
        (int)Math.Round(
            items
                .Where(i => i.WatchStatus == WatchStatus.Watched && i.Movie.RuntimeMinutes.HasValue)
                .Select(i => i.Movie.RuntimeMinutes!.Value)
                .DefaultIfEmpty(0)
                .Average()
        );

    public Dictionary<string, int> WatchedGenresCount(IEnumerable<WatchListItem> items) =>
        items
            .Where(i => i.WatchStatus == WatchStatus.Watched)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

    public Dictionary<string, int> ToWatchGenresCount(IEnumerable<WatchListItem> items) =>
        items
            .Where(i => i.WatchStatus == WatchStatus.ToWatch)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

    public Dictionary<string, int> FavoriteGenresCount(IEnumerable<WatchListItem> items) =>
        items
            .Where(i => i.IsFavourite)
            .SelectMany(i => i.Movie.Genres)
            .GroupBy(g => g)
            .ToDictionary(g => g.Key, g => g.Count());

    public Dictionary<string, int> AverageRuntimePerGenre(IEnumerable<WatchListItem> items) =>
        items
            .Where(i => i.WatchStatus == WatchStatus.Watched && i.Movie.RuntimeMinutes.HasValue)
            .SelectMany(i => i.Movie.Genres.Select(g => new { Genre = g, Runtime = i.Movie.RuntimeMinutes!.Value }))
            .GroupBy(x => x.Genre)
            .ToDictionary(g => g.Key, g => (int)Math.Round(g.Average(x => x.Runtime)));

    public double FavoriteRate(IEnumerable<WatchListItem> items)
    {
        var totalWatched = items.Count(i => i.WatchStatus == WatchStatus.Watched);
        if (totalWatched == 0)
        {
            return 0;
        }

        var totalFavorites = items.Count(i => i.IsFavourite);
        return Math.Round((double)totalFavorites / totalWatched, 3);
    }

    public int TotalWatchedRuntimeMinutes(IEnumerable<WatchListItem> items)
    {
        return items
            .Where(i => i.WatchStatus == WatchStatus.Watched && i.Movie.RuntimeMinutes.HasValue)
            .Sum(i => i.Movie.RuntimeMinutes!.Value);
    }
}
