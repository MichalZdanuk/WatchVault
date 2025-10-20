using Microsoft.EntityFrameworkCore;
using WatchVault.Application.Repositories;
using WatchVault.Domain.Entities;

namespace WatchVault.Infrastructure.Data.Repositories;
public class WatchListRepository(WatchVaultDbContext dbContext)
    : IWatchListRepository
{
    public async Task<WatchList?> GetByUserIdAsync(Guid userId)
    {
        return await dbContext.WatchLists
            .Include(x => x.Items)
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public Task AddAsync(WatchList watchList)
    {
        return dbContext.WatchLists.AddAsync(watchList).AsTask();
    }

    public Task UpdateAsync(WatchList watchList)
    {
        dbContext.WatchLists.Update(watchList);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyCollection<WatchListItem>> GetWatchedItemsInRangeAsync(Guid userId, DateTime start, DateTime end)
    {
        var watchListId = await dbContext.WatchLists
            .Where(wl => wl.UserId == userId)
            .Select(wl => wl.Id)
            .SingleOrDefaultAsync();

        if (watchListId == Guid.Empty)
        {
            return Array.Empty<WatchListItem>();
        }

        var items = await dbContext.WatchListItems
            .AsNoTracking()
            .Where(i => i.WatchListId == watchListId
                        && i.WatchStatus == Domain.Enums.WatchStatus.Watched
                        && i.WatchedAt.HasValue
                        && i.WatchedAt.Value >= start
                        && i.WatchedAt.Value <= end)
            .ToListAsync();

        return items;
    }

    public async Task<IReadOnlyCollection<WatchListItem>> GetWatchlistHistoryAsync(Guid userId, int pageNumber, int pageSize)
    {
        var watchListId = await dbContext.WatchLists
            .Where(wl => wl.UserId == userId)
            .Select(wl => wl.Id)
            .SingleOrDefaultAsync();

        if (watchListId == Guid.Empty)
        {
            return Array.Empty<WatchListItem>();
        }

        var items = await dbContext.WatchListItems
            .AsNoTracking()
            .Where(wli => wli.WatchListId == watchListId)
            .OrderByDescending(wli => wli.UpdateDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return items;
    }

    public async Task<int> GetWatchlistHistoryCountAsync(Guid userId)
    {
        var watchListId = await dbContext.WatchLists
            .Where(wl => wl.UserId == userId)
            .Select(wl => wl.Id)
            .SingleOrDefaultAsync();

        if (watchListId == Guid.Empty)
            return 0;

        var count = await dbContext.WatchListItems
            .AsNoTracking()
            .Where(wli => wli.WatchListId == watchListId)
            .CountAsync();

        return count;
    }
}
