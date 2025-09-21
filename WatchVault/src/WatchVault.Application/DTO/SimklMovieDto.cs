namespace WatchVault.Application.DTO;
public record SimklMovieDto(int simklId, string title, int year, string posterUrl,
        DateTime releaseDate, int runtimeMinutes, string director, string overview);
