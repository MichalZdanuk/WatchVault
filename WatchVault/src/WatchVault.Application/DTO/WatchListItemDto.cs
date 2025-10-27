namespace WatchVault.Application.DTO;
public record WatchListItemDto(Guid Id, Enums.WatchStatus WatchStatus, DateTime? AddedToWatchAt, DateTime? WatchedAt, bool IsFavourite, MovieWatchlistItemDto Movie);
