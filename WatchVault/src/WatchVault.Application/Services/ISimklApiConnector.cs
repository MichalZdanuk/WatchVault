using WatchVault.Application.ValueObjects;
using WatchVault.Shared.Pagination;

namespace WatchVault.Application.Services;
public interface ISimklApiConnector
{
    Task<PagedResponse<SimklSearchMovie>> SearchMoviesAsync(string query, int page = 1, int limit = 10);
    Task<SimklMovieDetails?> GetMovieDetailsAsync(int simklId);
    Task<PagedResponse<SimklTrendingMovie>> GetTrendingMoviesAsync(string period = "month");
}
