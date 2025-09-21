using WatchVault.Domain.Enums;

namespace WatchVault.Application.DTO;
public record WatchListItemDto(Guid Id, WatchStatus WatchStatus, DateTime? AddedToWatchAt, DateTime? WatchedAt, SimklMovieDto Movie);
