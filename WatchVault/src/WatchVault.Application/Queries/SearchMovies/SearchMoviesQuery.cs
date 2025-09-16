namespace WatchVault.Application.Queries.SearchMovies;
public record SearchMoviesQuery(string SearchTerm, int Page = 1) : IQuery<List<MovieDto>>;
