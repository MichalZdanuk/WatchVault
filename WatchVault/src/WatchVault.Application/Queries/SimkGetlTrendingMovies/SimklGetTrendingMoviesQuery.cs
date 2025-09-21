namespace WatchVault.Application.Queries.SimkGetlTrendingMovies;
public record SimklGetTrendingMoviesQuery(string Period = "month")
    : IQuery<List<SimklTrendingMovieDto>>;
