namespace WatchVault.Application.DTO;
public record SimklMovieDetailsDto(
    int SimklId,
    string Title,
    int Year,
    string Type,
    string PosterUrl,
    DateTime Released,
    int Runtime,
    double? ImdbRating,
    string Director,
    string Overview,
    List<string> Genres
);
