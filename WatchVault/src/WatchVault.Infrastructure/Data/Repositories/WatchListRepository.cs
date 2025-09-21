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
}
