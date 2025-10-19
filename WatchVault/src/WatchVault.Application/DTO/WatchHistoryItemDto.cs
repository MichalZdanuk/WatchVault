using WatchVault.Application.Enums;

namespace WatchVault.Application.DTO;
public record WatchHistoryItemDto(
    Guid Id,
    DateTime? AddedToWatchAt,
    DateTime? WatchedAt,
    bool IsFavourite,
    int SimklId,
    string Title,
    string PosterUrl,
    IReadOnlyList<string> Genres,
    Status Status
);
