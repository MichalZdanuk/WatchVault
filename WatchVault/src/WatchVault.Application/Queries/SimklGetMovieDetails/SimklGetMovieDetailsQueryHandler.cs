using WatchVault.Application.Exceptions;
using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.SimklGetMovieDetails;
public class SimklGetMovieDetailsQueryHandler(ISimklApiConnector simkl)
    : IQueryHandler<SimklGetMovieDetailsQuery, SimklMovieDetailsDto>
{
    public async Task<SimklMovieDetailsDto> Handle(SimklGetMovieDetailsQuery query, CancellationToken ct)
    {
        var raw = await simkl.GetMovieDetailsAsync(query.SimklId);
        if (raw is null)
        {
            throw new MovieNotFoundException(query.SimklId);
        }

        return new SimklMovieDetailsDto(
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
            raw.Genres.ToList(),
            raw.UserRecommendations.Select(x => new UserRecommendationDto(x.Title, x.Year, x.Poster, x.SimklId)).ToList()
        );
    }
}
