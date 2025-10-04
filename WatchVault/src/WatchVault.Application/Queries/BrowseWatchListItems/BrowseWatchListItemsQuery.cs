using WatchVault.Application.Enums;

namespace WatchVault.Application.Queries.BrowseWatchListItems;
public record BrowseWatchListItemsQuery(Status? Status, int PageNumber, int PageSize) : IQuery<PagedWatchListItemsDto>;
