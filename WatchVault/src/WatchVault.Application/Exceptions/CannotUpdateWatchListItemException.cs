using WatchVault.Shared.Exceptions;

namespace WatchVault.Application.Exceptions;
public sealed class CannotUpdateWatchListItemException(Guid Id)
    : BadRequestException($"Watchlist item: {Id} cannot be updated due to invalid watchstatus")
{
    public Guid Id { get; } = Id;
}
