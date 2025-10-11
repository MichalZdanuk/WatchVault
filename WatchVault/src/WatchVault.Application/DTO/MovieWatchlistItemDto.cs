namespace WatchVault.Application.DTO;
public record MovieWatchlistItemDto(int simklId, string title, int year, string posterUrl,
        DateTime? releaseDate, int? runtimeMinutes, string director, string overview);
