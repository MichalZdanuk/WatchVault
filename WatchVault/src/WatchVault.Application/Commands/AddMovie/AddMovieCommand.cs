using WatchVault.Application.Enums;

namespace WatchVault.Application.Commands.AddMovie;
public sealed record AddMovieCommand(int SimklId, WatchStatus WatchStatus) : ICommand<Guid>;
