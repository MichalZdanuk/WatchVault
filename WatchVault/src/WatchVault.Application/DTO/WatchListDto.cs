namespace WatchVault.Application.DTO;
public record WatchListDto(Guid Id, int TotalWatched, int TotalToWatch, IList<WatchListItemDto> WatchListItems);
