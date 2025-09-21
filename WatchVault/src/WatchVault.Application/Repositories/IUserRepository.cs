using WatchVault.Domain.Entities;

namespace WatchVault.Application.Repositories;
public interface IUserRepository
{
    public Task AddAsync(User user);
    public Task<User?> GetByIdAsync(Guid id);
}
