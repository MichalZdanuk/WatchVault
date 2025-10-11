namespace WatchVault.Application.Queries.GetMovieDetails;
public record GetMovieDetailsQuery(int SimklId) : IQuery<MovieDetailsDto>;
