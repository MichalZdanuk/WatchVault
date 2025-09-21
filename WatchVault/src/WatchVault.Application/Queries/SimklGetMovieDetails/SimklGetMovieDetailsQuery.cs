namespace WatchVault.Application.Queries.SimklGetMovieDetails;
public record SimklGetMovieDetailsQuery(int SimklId) : IQuery<SimklMovieDetailsDto>;
