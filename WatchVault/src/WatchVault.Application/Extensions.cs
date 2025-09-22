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

    private static DistributedCacheEntryOptions CacheOptions = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(30))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

    public static async Task<T?> GetOrSetAsync<T>(this IDistributedCache cache, string cacheKey, Func<Task<T>> task)
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
            await cache.SetAsync(cacheKey, bytes, CacheOptions);
        }

        return result;
    }
}
