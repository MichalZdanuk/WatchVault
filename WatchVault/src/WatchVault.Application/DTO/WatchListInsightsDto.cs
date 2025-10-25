namespace WatchVault.Application.DTO;
public record WatchListInsightsDto(
    int TotalWatched,
    int TotalToWatch,
    int TotalFavorites,
    double FavoriteRate,
    int AverageRuntimeMinutes,
    int TotalWatchedRuntimeMinutes,
    Dictionary<string, int> WatchedGenresCount,
    Dictionary<string, int> ToWatchGenresCount,
    Dictionary<string, int> FavoriteGenresCount,
    Dictionary<string, int> AverageRuntimePerGenre,
    Dictionary<string, int> WatchedDayOfWeekDistribution,
    MostWatchedDayDto MostWatchedDay
);

public record MostWatchedDayDto(string Day, string Label, int WatchedCount, int WatchedPercentage);