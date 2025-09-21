using Microsoft.EntityFrameworkCore;
using WatchVault.Application.Repositories;
using WatchVault.Domain.Entities;

namespace WatchVault.Infrastructure.Data.Repositories;
public class UserRepository : IUserRepository
{
    private readonly WatchVaultDbContext _context;

    public UserRepository(WatchVaultDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> GetByExternalIdAsync(Guid externalId)
    {
        return await _context.Users
            .SingleOrDefaultAsync(u => u.ExternalId == externalId);
    }
}
