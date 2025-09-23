using WatchVault.Application.Common;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Commands.RemoveMovie;
public sealed class RemoveWatchListItemCommandHandler(IUnitOfWork unitOfWork,
    IUserContext userContext)
    : ICommandHandler<RemoveWatchListItemCommand, bool>
{
    public async Task<bool> Handle(RemoveWatchListItemCommand command, CancellationToken cancellationToken)
    {
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);
        if (watchList is null)
        {
            throw new Exception($"User {userContext.UserId} has no watch list.");
        }

        watchList.RemoveWatchListItem(command.watchListItemId);

        await unitOfWork.WatchListRepository.UpdateAsync(watchList);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
