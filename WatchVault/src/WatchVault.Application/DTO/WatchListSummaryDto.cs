namespace WatchVault.Application.DTO;
public record WatchListSummaryDto(Guid Id,
    int TotalWatched,
    int TotalToWatch,
    int TotalRuntimeMinutes,
    double AverageRuntimeMinutes,
    int? EarliestYearWatched,
    int? LatestYearWatched,
    DateTime? LastWatchedAt,
    DateTime? LastAddedToWatchAt);
