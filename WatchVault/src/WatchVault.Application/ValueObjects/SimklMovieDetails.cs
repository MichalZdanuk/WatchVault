namespace WatchVault.Application.ValueObjects;
public record SimklMovieDetails(
    string Title,
    int Year,
    string Type,
    int SimklId,
    string Poster,
    DateTime ReleaseDate,
    int RuntimeMinutes,
    double? ImdbRating,
    string Director,
    string Overview,
    IReadOnlyList<string> Genres
);
