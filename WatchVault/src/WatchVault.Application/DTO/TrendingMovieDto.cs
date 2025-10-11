namespace WatchVault.Application.DTO;
public record TrendingMovieDto(
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
