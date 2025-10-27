using WatchVault.Application.Enums;

namespace WatchVault.Application.DTO;
public record MovieDetailsDto(
    int SimklId,
    string Title,
    int Year,
    string Type,
    string PosterUrl,
    string FanartUrl,
    DateTime? Released,
    int? Runtime,
    double? ImdbRating,
    string Director,
    string Overview,
    WatchStatus? WatchStatus,
    List<string> Genres,
    List<UserRecommendationDto> UserRecommendations
);
