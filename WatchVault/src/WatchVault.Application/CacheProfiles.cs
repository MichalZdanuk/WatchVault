using Microsoft.Extensions.Caching.Distributed;

namespace WatchVault.Application;
public static class CacheProfiles
{
    public static readonly DistributedCacheEntryOptions Default = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(30))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(60));

    public static readonly DistributedCacheEntryOptions ShortLived = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(20));

    public static readonly DistributedCacheEntryOptions Analytics = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromHours(1))
        .SetAbsoluteExpiration(TimeSpan.FromHours(12));
}

