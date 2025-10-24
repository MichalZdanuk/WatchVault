using WatchVault.Domain.Entities;

namespace WatchVault.Application.Factories;
public interface IWatchListInsightsFactory
{
    int TotalWatched(IEnumerable<WatchListItem> items);
    int TotalToWatch(IEnumerable<WatchListItem> items);
    int TotalFavorites(IEnumerable<WatchListItem> items);
    int AverageRuntime(IEnumerable<WatchListItem> items);
    Dictionary<string, int> WatchedGenresCount(IEnumerable<WatchListItem> items);
    Dictionary<string, int> ToWatchGenresCount(IEnumerable<WatchListItem> items);
    Dictionary<string, int> FavoriteGenresCount(IEnumerable<WatchListItem> items);
    Dictionary<string, int> AverageRuntimePerGenre(IEnumerable<WatchListItem> items);
    public double FavoriteRate(IEnumerable<WatchListItem> items);
    public int TotalWatchedRuntimeMinutes(IEnumerable<WatchListItem> items);
}
