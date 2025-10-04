namespace WatchVault.Application.Commands.ToggleFavourite;
public record ToggleFavouriteCommand(Guid WatchListItemId) : ICommand<bool>;
