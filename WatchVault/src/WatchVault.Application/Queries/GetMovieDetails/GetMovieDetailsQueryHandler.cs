using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Repositories;
using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.GetMovieDetails;
public class GetMovieDetailsQueryHandler(ISimklApiConnector simkl,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IQueryHandler<GetMovieDetailsQuery, MovieDetailsDto>
{
    public async Task<MovieDetailsDto> Handle(GetMovieDetailsQuery query, CancellationToken ct)
    {
        var raw = await simkl.GetMovieDetailsAsync(query.SimklId);
        if (raw is null)
        {
            throw new MovieNotFoundException(query.SimklId);
        }

        var watchlist = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId)
            ?? throw new Exception($"User {userContext.UserId} has no watch list.");

        var existing = watchlist.Items.FirstOrDefault(i => i.Movie.SimklId == query.SimklId);
        var status = existing?.WatchStatus;

        return new MovieDetailsDto(
            raw.SimklId,
            raw.Title,
            raw.Year,
            raw.Type,
            raw.Poster,
            raw.Fanart,
            raw.ReleaseDate,
            raw.RuntimeMinutes,
            raw.ImdbRating,
            raw.Director,
            raw.Overview,
            status.HasValue ? status.Value.ConvertToApplicationWatchStatus() : null,
            raw.Genres.ToList(),
            raw.UserRecommendations.Select(x => new UserRecommendationDto(x.Title, x.Year, x.Poster, x.SimklId)).ToList()
        );
    }
}
