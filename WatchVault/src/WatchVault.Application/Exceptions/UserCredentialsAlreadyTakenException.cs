using WatchVault.Shared.Exceptions;

namespace WatchVault.Application.Exceptions;
public sealed class UserCredentialsAlreadyTakenException(string message)
    : ConflictException(message)
{
}
