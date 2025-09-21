namespace WatchVault.Shared.Pagination;
public record PagedResponse<T>(
    int PageNumber,
    int PageSize,
    IReadOnlyList<T> Items
);
