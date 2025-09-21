using WatchVault.Application.Repositories;

namespace WatchVault.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly WatchVaultDbContext Context;
    public IUserRepository UserRepository { get; }

    public UnitOfWork(WatchVaultDbContext context, IUserRepository userRepository)
    {
        Context = context;
        UserRepository = userRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}
