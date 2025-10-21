namespace WatchVault.Application.DTO;
public record WatchHistoryItemDto(
    Guid Id,
    DateTime WatchedAt,
    bool IsFavourite,
    int SimklId,
    string Title,
    string PosterUrl,
    IReadOnlyList<string> Genres
);
