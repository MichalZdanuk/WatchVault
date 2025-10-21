using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WatchVault.Application;
public static class Extensions
{
    private static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = null,
        WriteIndented = true,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Gets or sets a cached value with optional cache configuration.
    /// If <paramref name="options"/> is null, uses default (30/60 min).
    /// </summary>
    public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache,
        string cacheKey,
        Func<Task<T>> task,
        DistributedCacheEntryOptions? options = null)
    {
        var cachedBytes = await cache.GetAsync(cacheKey);

        if (cachedBytes is not null)
        {
            var cached = JsonSerializer.Deserialize<T>(cachedBytes, SerializerOptions);
            if (cached is not null)
            {
                return cached;
            }
        }

        var result = await task();
        if (result is not null)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result, SerializerOptions));
            await cache.SetAsync(cacheKey, bytes, options ?? CacheProfiles.Default);
        }

        return result;
    }
}
