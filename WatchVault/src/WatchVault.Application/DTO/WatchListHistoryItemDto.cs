namespace WatchVault.Application.DTO;
public record WatchListHistoryItemDto(
    Guid Id,
    DateTime WatchedAt,
    bool IsFavourite,
    int SimklId,
    string Title,
    string PosterUrl,
    IReadOnlyList<string> Genres
);
