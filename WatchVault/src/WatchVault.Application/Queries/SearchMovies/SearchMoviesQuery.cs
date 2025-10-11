namespace WatchVault.Application.Queries.SearchMovies;
public record SearchMoviesQuery(string SearchTerm, int Page = 1, int PageSize = 10)
    : IQuery<List<SearchMovieDto>>;
