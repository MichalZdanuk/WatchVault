using WatchVault.Application.Enums;

namespace WatchVault.Application.Queries.BrowseWatchListItems;
public record BrowseWatchListItemsQuery(WatchStatus? WatchStatus, int PageNumber, int PageSize) : IQuery<PagedWatchListItemsDto>;
