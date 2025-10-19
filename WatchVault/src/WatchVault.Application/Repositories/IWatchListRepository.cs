using WatchVault.Domain.Entities;

namespace WatchVault.Application.Repositories;
public interface IWatchListRepository
{
    Task<WatchList?> GetByUserIdAsync(Guid userId);
    Task AddAsync(WatchList watchList);
    Task UpdateAsync(WatchList watchList);
    Task<IReadOnlyCollection<WatchListItem>> GetWatchedItemsInRangeAsync(Guid userId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<WatchListItem>> GetWatchlistHistoryAsync(Guid userId, int pageNumber, int pageSize);
}
