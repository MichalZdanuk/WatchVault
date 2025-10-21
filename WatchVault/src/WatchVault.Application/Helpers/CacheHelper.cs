using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace WatchVault.Application.Helpers;
public static class CacheHelper
{
    private const string UserHistoryKeysPrefix = "history-keys-";

    public static async Task TrackUserHistoryKeyAsync(IDistributedCache cache, Guid userId, string cacheKey)
    {
        var indexKey = UserHistoryKeysPrefix + userId;
        var keysJson = await cache.GetStringAsync(indexKey);
        var keys = keysJson is null ? new HashSet<string>() : JsonSerializer.Deserialize<HashSet<string>>(keysJson)!;
        keys.Add(cacheKey);
        await cache.SetStringAsync(indexKey, JsonSerializer.Serialize(keys));
    }

    public static async Task ClearUserHistoryAsync(IDistributedCache cache, Guid userId)
    {
        var indexKey = UserHistoryKeysPrefix + userId;
        var keysJson = await cache.GetStringAsync(indexKey);
        if (keysJson is null) return;

        var keys = JsonSerializer.Deserialize<HashSet<string>>(keysJson)!;

        foreach (var key in keys)
        {
            await cache.RemoveAsync(key);
        }

        await cache.RemoveAsync(indexKey);
    }
}
