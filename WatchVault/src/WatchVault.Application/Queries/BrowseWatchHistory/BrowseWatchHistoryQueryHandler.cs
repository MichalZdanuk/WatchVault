using Microsoft.Extensions.Caching.Distributed;
using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.BrowseWatchHistory;
public sealed class BrowseWatchHistoryQueryHandler(IUserContext userContext,
    IDistributedCache cache,
    IUnitOfWork unitOfWork) : IQueryHandler<BrowseWatchHistoryQuery, WatchListHistoryDto>
{
    public async Task<WatchListHistoryDto> Handle(BrowseWatchHistoryQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var cacheKey = $"history-{userId}-{query.PageNumber}-{query.PageSize}";

        var result = await cache.GetOrSetAsync(cacheKey, async () =>
        {
            var items = await unitOfWork.WatchListRepository
                .GetWatchlistHistoryAsync(userId, query.PageNumber, query.PageSize);

            var dtos = items.Select(x =>
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

            var totalCount = await unitOfWork.WatchListRepository
                .GetWatchlistHistoryCountAsync(userId);

            return new WatchListHistoryDto(query.PageNumber, query.PageSize, totalCount, dtos);
        }, CacheProfiles.ShortLived);

        return result ?? new WatchListHistoryDto(query.PageNumber, query.PageSize, 0, []);
    }
}
