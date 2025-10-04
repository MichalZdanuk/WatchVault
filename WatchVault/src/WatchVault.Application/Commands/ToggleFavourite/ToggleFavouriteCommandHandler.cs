using WatchVault.Application.Common;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Commands.ToggleFavourite;
public sealed class ToggleFavouriteCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ToggleFavouriteCommand, bool>
{
    public async Task<bool> Handle(ToggleFavouriteCommand command, CancellationToken cancellationToken)
    {
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);
        if (watchList is null)
        {
            throw new Exception($"User {userContext.UserId} has no watch list.");
        }

        var item = watchList.Items.SingleOrDefault(x => x.Id == command.WatchListItemId);

        if (item is null)
        {
            throw new WatchListItemNotFroundException(command.WatchListItemId);
        }

        item.ToggleFavourite();
        await unitOfWork.WatchListRepository.UpdateAsync(watchList);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
