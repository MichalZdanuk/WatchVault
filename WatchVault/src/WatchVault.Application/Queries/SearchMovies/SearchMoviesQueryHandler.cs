using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.SearchMovies;
public class SearchMoviesQueryHandler(ISimklApiConnector simkl)
    : IQueryHandler<SearchMoviesQuery, List<SearchMovieDto>>
{
    public async Task<List<SearchMovieDto>> Handle(SearchMoviesQuery query, CancellationToken ct)
    {
        var response = await simkl.SearchMoviesAsync(query.SearchTerm, query.Page, query.PageSize);

        return response.Items.Select(m => new SearchMovieDto(
            m.SimklId,
            m.Title,
            m.Year,
            m.Poster
        )).ToList();
    }
}
