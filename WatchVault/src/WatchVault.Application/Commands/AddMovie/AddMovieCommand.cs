using WatchVault.Application.Enums;

namespace WatchVault.Application.Commands.AddMovie;
public sealed record AddMovieCommand(int SimklId, Status WatchStatus) : ICommand<Guid>;
