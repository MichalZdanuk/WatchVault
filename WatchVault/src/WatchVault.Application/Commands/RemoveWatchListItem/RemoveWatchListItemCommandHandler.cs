using Microsoft.Extensions.Caching.Distributed;
using WatchVault.Application.Common;
using WatchVault.Application.Helpers;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Commands.RemoveMovie;
public sealed class RemoveWatchListItemCommandHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IDistributedCache cache)
    : ICommandHandler<RemoveWatchListItemCommand, bool>
{
    public async Task<bool> Handle(RemoveWatchListItemCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userId);
        if (watchList is null)
        {
            throw new Exception($"User {userId} has no watch list.");
        }

        watchList.RemoveWatchListItem(command.watchListItemId);

        await unitOfWork.WatchListRepository.UpdateAsync(watchList);
        await unitOfWork.SaveChangesAsync();

        await CacheHelper.ClearUserHistoryAsync(cache, userId);

        return true;
    }
}
