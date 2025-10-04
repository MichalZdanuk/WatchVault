namespace WatchVault.Application.DTO;
public record PagedWatchListItemsDto(
    int PageNumber,
    int PageSize,
    int TotalCount,
    IReadOnlyList<WatchListItemDto> Items
);
