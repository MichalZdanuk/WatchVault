namespace WatchVault.Application.DTO;
public record WatchListHistoryDto(
    int PageNumber,
    int PageSize,
    int TotalCount,
    IReadOnlyList<WatchHistoryItemDto> Items
)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}