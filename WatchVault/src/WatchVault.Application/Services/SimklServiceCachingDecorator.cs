using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WatchVault.Application.ValueObjects;
using WatchVault.Shared.Pagination;

namespace WatchVault.Application.Services;
public class SimklServiceCachingDecorator(ISimklApiConnector simklApiConnector,
    IDistributedCache cache)
    : ISimklApiConnector
{
    private static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = null,
        WriteIndented = true,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private static DistributedCacheEntryOptions CacheOptions = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(30))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

    public async Task<SimklMovieDetails?> GetMovieDetailsAsync(int simklId)
    {
        var cacheKey = $"movies-{simklId}";
        var cachedBytes = await cache.GetAsync(cacheKey);

        if (cachedBytes is not null)
        {
            var cachedObj = JsonSerializer.Deserialize<SimklMovieDetails>(cachedBytes, SerializerOptions);
            if (cachedObj is not null)
            {
                return cachedObj;
            }
        }

        var movieDetails = await simklApiConnector.GetMovieDetailsAsync(simklId);
        if (movieDetails is null)
        {
            return null;
        }

        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(movieDetails, SerializerOptions));
        await cache.SetAsync(cacheKey, bytes, CacheOptions);

        return movieDetails;
    }

    public async Task<PagedResponse<SimklTrendingMovie>> GetTrendingMoviesAsync(string period = "month")
    {
        var cacheKey = $"movies-trendning-{period}";
        var cachedBytes = await cache.GetAsync(cacheKey);

        if (cachedBytes is not null)
        {
            var cachedObj = JsonSerializer.Deserialize<PagedResponse<SimklTrendingMovie>>(cachedBytes, SerializerOptions);
            if (cachedObj is not null)
            {
                return cachedObj;
            }
        }

        var trendingMovies = await simklApiConnector.GetTrendingMoviesAsync(period);
        if (trendingMovies.Items.Count == 0)
        {
            return trendingMovies;
        }

        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(trendingMovies, SerializerOptions));
        await cache.SetAsync(cacheKey, bytes, CacheOptions);

        return trendingMovies;
    }

    public async Task<PagedResponse<SimklSearchMovie>> SearchMoviesAsync(string query, int page = 1, int limit = 10)
    {
        var cacheKey = $"movies-query-{query}-page-{page}-limit-{limit}";
        var cachedBytes = await cache.GetAsync(cacheKey);

        if (cachedBytes is not null)
        {
            var cachedObj = JsonSerializer.Deserialize<PagedResponse<SimklSearchMovie>>(cachedBytes, SerializerOptions);
            if (cachedObj is not null)
            {
                return cachedObj;
            }
        }

        var simklSearchMovies = await simklApiConnector.SearchMoviesAsync(query, page, limit);
        if (simklSearchMovies.Items.Count == 0)
        {
            return simklSearchMovies;
        }

        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(simklSearchMovies, SerializerOptions));
        await cache.SetAsync(cacheKey, bytes, CacheOptions);

        return simklSearchMovies;
    }
}
