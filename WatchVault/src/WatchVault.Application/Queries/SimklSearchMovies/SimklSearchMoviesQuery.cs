namespace WatchVault.Application.Queries.SimklSearchMovies;
public record SimklSearchMoviesQuery(string SearchTerm, int Page = 1, int PageSize = 10)
    : IQuery<List<SimklSearchMovieDto>>;
