namespace WatchVault.Application.Commands.RemoveMovie;
public record RemoveWatchListItemCommand(Guid watchListItemId) : ICommand<bool>;
