using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.BrowseWatchHistory;
public sealed class BrowseWatchHistoryQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IQueryHandler<BrowseWatchHistoryQuery, WatchListHistoryDto>
{
    public async Task<WatchListHistoryDto> Handle(BrowseWatchHistoryQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var items = await unitOfWork.WatchListRepository.GetWatchlistHistoryAsync(userId, query.PageNumber, query.PageSize);

        IReadOnlyList<WatchHistoryItemDto> dtos = items.Select(x =>
            new WatchHistoryItemDto(
                x.Id,
                x.AddedToWatchAt,
                x.WatchedAt,
                x.IsFavourite,
                x.Movie.SimklId,
                x.Movie.Title,
                x.Movie.PosterUrl,
                x.Movie.Genres,
                x.WatchStatus.ConvertToApplicationWatchStatus()
            )
        ).ToList();

        var totalCount = await unitOfWork.WatchListRepository.GetWatchlistHistoryCountAsync(userId);

        return new WatchListHistoryDto(
            query.PageNumber,
            query.PageSize,
            totalCount,
            dtos
        );
    }
}
