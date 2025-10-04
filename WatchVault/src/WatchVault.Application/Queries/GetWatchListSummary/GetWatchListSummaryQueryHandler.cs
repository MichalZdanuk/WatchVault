using WatchVault.Application.Common;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.GetWatchList;
public sealed class GetWatchListSummaryQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetWatchListSummaryQuery, WatchListSummaryDto>
{
    public async Task<WatchListSummaryDto> Handle(GetWatchListSummaryQuery query, CancellationToken cancellationToken)
    {
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);

        if (watchList is null)
        {
            throw new InvalidOperationException($"User {userContext.UserId} has no watchlist.");
        }

        return new WatchListSummaryDto(watchList.Id, watchList.TotalWatched, watchList.TotalToWatch);
    }
}
