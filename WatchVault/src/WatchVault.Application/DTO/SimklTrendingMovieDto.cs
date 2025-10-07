namespace WatchVault.Application.DTO;
public record SimklTrendingMovieDto(
    int SimklId,
    string Title,
    string PosterUrl,
    string ReleaseDate,
    double? ImdbRating,
    string Runtime,
    int RuntimeMinutes,
    string Overview,
    string Metadata
);
