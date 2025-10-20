namespace WatchVault.Application.Commands.EditWatchDate;
public record EditWatchDateCommand(Guid WatchListItemId, DateTime WatchedAtDate) : ICommand<bool>;
