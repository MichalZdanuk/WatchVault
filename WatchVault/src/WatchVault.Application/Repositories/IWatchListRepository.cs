using WatchVault.Domain.Entities;

namespace WatchVault.Application.Repositories;
public interface IWatchListRepository
{
    Task<WatchList?> GetByUserIdAsync(Guid userId);
    Task AddAsync(WatchList watchList);
    Task UpdateAsync(WatchList watchList);
}
