namespace WatchVault.Application.Queries.BrowseWatchHistory;
public record BrowseWatchHistoryQuery(int PageNumber = 1, int PageSize = 20) : IQuery<WatchListHistoryDto>;
