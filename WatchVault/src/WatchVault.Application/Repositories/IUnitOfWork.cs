namespace WatchVault.Application.Repositories;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
