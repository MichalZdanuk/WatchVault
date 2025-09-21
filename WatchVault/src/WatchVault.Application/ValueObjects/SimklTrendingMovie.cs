namespace WatchVault.Application.ValueObjects;
public record SimklTrendingMovie(
    string Title,
    string Url,
    string Poster,
    int SimklId,
    string ReleaseDate,
    double? ImdbRating,
    string Runtime,
    string Overview,
    string Metadata
);
