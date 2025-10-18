namespace WatchVault.Application.DTO;
public record WatchListInsightsDto(
    int TotalWatched,
    int TotalToWatch,
    int TotalFavorites,
    int AverageRuntimeMinutes,
    Dictionary<string, int> WatchedGenresCount,
    Dictionary<string, int> ToWatchGenresCount,
    Dictionary<string, int> FavoriteGenresCount,
    Dictionary<string, int> AverageRuntimePerGenre
);
