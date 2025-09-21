using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.SimkGetlTrendingMovies;
public class SimklTrendingMoviesQueryHandler(ISimklApiConnector simkl)
    : IQueryHandler<SimklGetTrendingMoviesQuery, List<SimklTrendingMovieDto>>
{
    public async Task<List<SimklTrendingMovieDto>> Handle(SimklGetTrendingMoviesQuery query, CancellationToken ct)
    {
        var response = await simkl.GetTrendingMoviesAsync(query.Period);

        return response.Items.Select(m => new SimklTrendingMovieDto(
            m.SimklId,
            m.Title,
            m.Poster,
            m.ReleaseDate,
            m.ImdbRating,
            m.Runtime,
            m.Overview,
            m.Metadata
        )).ToList();
    }
}
