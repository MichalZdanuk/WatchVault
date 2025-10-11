using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.GetTrendingMovies;
public class SimklTrendingMoviesQueryHandler(ISimklApiConnector simkl)
    : IQueryHandler<GetTrendingMoviesQuery, List<TrendingMovieDto>>
{
    public async Task<List<TrendingMovieDto>> Handle(GetTrendingMoviesQuery query, CancellationToken ct)
    {
        var response = await simkl.GetTrendingMoviesAsync(query.Period);

        return response.Items.Select(m => new TrendingMovieDto(
            m.SimklId,
            m.Title,
            m.Poster,
            m.ReleaseDate,
            m.ImdbRating,
            m.Runtime,
            m.RuntimeMinutes,
            m.Overview,
            m.Metadata
        )).ToList();
    }
}
