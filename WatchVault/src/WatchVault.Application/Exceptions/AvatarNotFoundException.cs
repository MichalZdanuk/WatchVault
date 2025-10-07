using WatchVault.Shared.Exceptions;

namespace WatchVault.Application.Exceptions;
public sealed class AvatarNotFoundException(Guid userId)
    : NotFoundException($"Avatar for user: {userId} was not found.")
{
    public Guid UserId { get; } = userId;
}
