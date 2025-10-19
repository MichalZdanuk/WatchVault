using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Repositories;
using WatchVault.Shared.Pagination;

namespace WatchVault.Application.Queries.BrowseWatchHistory;
public sealed class BrowseWatchHistoryQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IQueryHandler<BrowseWatchHistoryQuery, PagedResponse<WatchHistoryItemDto>>
{
    public async Task<PagedResponse<WatchHistoryItemDto>> Handle(BrowseWatchHistoryQuery query, CancellationToken cancellationToken)
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

        return new PagedResponse<WatchHistoryItemDto>(
            query.PageNumber,
            query.PageSize,
            dtos
        );
    }
}
