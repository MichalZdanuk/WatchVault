using Microsoft.Extensions.Caching.Distributed;
using WatchVault.Application.Common;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Helpers;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Commands.EditWatchDate;
public sealed class EditWatchDateCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,
    IDistributedCache cache)
    : ICommandHandler<EditWatchDateCommand, bool>
{
    public async Task<bool> Handle(EditWatchDateCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userId);

        if (watchList is null)
        {
            throw new Exception($"User {userId} has no watch list.");
        }

        var item = watchList.Items.FirstOrDefault(x => x.Id == command.WatchListItemId);

        if (item is null)
        {
            throw new WatchListItemNotFroundException(command.WatchListItemId);
        }

        item.TryUpdateWatchedAt(command.WatchedAtDate, out bool success);

        if (!success)
        {
            throw new CannotUpdateWatchListItemException(command.WatchListItemId);
        }

        await unitOfWork.WatchListRepository.UpdateAsync(watchList);
        await unitOfWork.SaveChangesAsync();

        await CacheHelper.ClearUserHistoryAsync(cache, userId);

        return true;
    }
}
