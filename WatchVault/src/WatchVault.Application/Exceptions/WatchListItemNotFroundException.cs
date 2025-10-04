using WatchVault.Shared.Exceptions;

namespace WatchVault.Application.Exceptions;
public class WatchListItemNotFroundException(Guid Id)
    : NotFoundException($"Watchlist item: {Id} was not found")
{
    public Guid Id { get; } = Id;
}
