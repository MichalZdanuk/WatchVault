using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.SimklSearchMovies;
public class SimklSearchMoviesQueryHandler(ISimklApiConnector simkl)
    : IQueryHandler<SimklSearchMoviesQuery, List<SimklSearchMovieDto>>
{
    public async Task<List<SimklSearchMovieDto>> Handle(SimklSearchMoviesQuery query, CancellationToken ct)
    {
        var response = await simkl.SearchMoviesAsync(query.SearchTerm, query.Page, query.PageSize);

        return response.Items.Select(m => new SimklSearchMovieDto(
            m.SimklId,
            m.Title,
            m.Year,
            $"https://simkl.in/posters/{m.Poster}_m.jpg"
        )).ToList();
    }
}
