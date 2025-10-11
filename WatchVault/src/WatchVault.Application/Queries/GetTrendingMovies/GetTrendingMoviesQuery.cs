namespace WatchVault.Application.Queries.GetTrendingMovies;
public record GetTrendingMoviesQuery(string Period = "month")
    : IQuery<List<TrendingMovieDto>>;
