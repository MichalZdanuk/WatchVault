using Microsoft.Extensions.Caching.Distributed;
using WatchVault.Application.ValueObjects;
using WatchVault.Shared.Pagination;

namespace WatchVault.Application.Services;
public class SimklServiceCachingDecorator(ISimklApiConnector simklApiConnector,
    IDistributedCache cache)
    : ISimklApiConnector
{
    public async Task<SimklMovieDetails?> GetMovieDetailsAsync(int simklId)
        => await cache.GetOrSetAsync($"movies-{simklId}", () => simklApiConnector.GetMovieDetailsAsync(simklId));

    public async Task<PagedResponse<SimklTrendingMovie>> GetTrendingMoviesAsync(string period = "month")
    {
        var result = await cache.GetOrSetAsync(
            $"movies-trending-{period.ToLowerInvariant()}",
            () => simklApiConnector.GetTrendingMoviesAsync(period));

        return result ?? new PagedResponse<SimklTrendingMovie>(1, 0, new List<SimklTrendingMovie>());
    }

    public async Task<PagedResponse<SimklSearchMovie>> SearchMoviesAsync(string query, int page = 1, int limit = 10)
    {
        var result = await cache.GetOrSetAsync($"movies-query-{Uri.EscapeDataString(query.ToLowerInvariant())}-page-{page}-limit-{limit}",
                () => simklApiConnector.SearchMoviesAsync(query, page, limit));

        return result ?? new PagedResponse<SimklSearchMovie>(1, 0, new List<SimklSearchMovie>());
    }
}
