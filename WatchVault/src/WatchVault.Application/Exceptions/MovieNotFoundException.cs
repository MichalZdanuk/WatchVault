using WatchVault.Shared.Exceptions;

namespace WatchVault.Application.Exceptions;
public class MovieNotFoundException(int movieId)
    : NotFoundException($"Movie with id: {movieId} was not found")
{
    public int MovieId { get; } = movieId;
}
