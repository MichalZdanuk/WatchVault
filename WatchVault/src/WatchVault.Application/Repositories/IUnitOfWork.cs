namespace WatchVault.Application.Repositories;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IWatchListRepository WatchListRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
