using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.SearchMovies;
public class SearchMoviesQueryHandler(IMovieApiService movieApiService)
    : IQueryHandler<SearchMoviesQuery, List<MovieDto>>
{
    public async Task<List<MovieDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
    {
        var omdbResponse = await movieApiService.SearchMoviesAsync(request.SearchTerm, request.Page, cancellationToken);

        if (omdbResponse.Response != "True" || omdbResponse.Search == null || !omdbResponse.Search.Any())
            return new List<MovieDto>();

        return omdbResponse.Search.Select(m => new MovieDto(
            m.imdbID,
            m.Title,
            m.Year,
            m.Type,
            m.Poster
        )).ToList();
    }
}
