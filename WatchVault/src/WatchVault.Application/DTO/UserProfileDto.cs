namespace WatchVault.Application.DTO;
public record UserProfileDto(
    Guid Id,
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    UserOverallStatisticsDto UserOverallStatistics,
    List<MovieSummaryDto> Watched,
    List<MovieSummaryDto> ToWatch
);

public record MovieSummaryDto(
    int SimklId,
    string Title,
    string PosterUrl
);

public record UserOverallStatisticsDto(
    int TotalWatched,
    int TotalToWatch,
    int TotalFavorites
);