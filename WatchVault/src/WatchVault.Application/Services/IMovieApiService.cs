using WatchVault.Application.ValueObjects;

namespace WatchVault.Application.Services;
public interface IMovieApiService
{
    Task<OmdbSearchResponse> SearchMoviesAsync(string searchTerm, int page = 1, CancellationToken cancellationToken = default);
}
