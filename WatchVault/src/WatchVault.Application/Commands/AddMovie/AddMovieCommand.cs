namespace WatchVault.Application.Commands.AddMovie;
public sealed record AddMovieCommand(int SimklId, bool MarkAsWatched) : ICommand<Guid>;
