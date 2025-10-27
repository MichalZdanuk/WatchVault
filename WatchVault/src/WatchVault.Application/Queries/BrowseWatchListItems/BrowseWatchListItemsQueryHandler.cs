
using WatchVault.Application.Common;
using WatchVault.Application.Enums;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.BrowseWatchListItems;
public sealed class BrowseWatchListItemsQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<BrowseWatchListItemsQuery, PagedWatchListItemsDto>
{
    public async Task<PagedWatchListItemsDto> Handle(BrowseWatchListItemsQuery query, CancellationToken cancellationToken)
    {
        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userContext.UserId);

        if (watchList is null)
        {
            throw new InvalidOperationException($"User {userContext.UserId} has no watchlist.");
        }

        var items = watchList.Items.AsQueryable();

        if (query.WatchStatus.HasValue)
        {
            items = items.Where(i => i.WatchStatus == query.WatchStatus.Value.ConvertToDomainWatchStatus()).AsQueryable();
        }

        items = query.WatchStatus switch
        {
            WatchStatus.Watched => items.OrderByDescending(i => i.WatchedAt ?? i.UpdateDate),
            WatchStatus.ToWatch => items.OrderByDescending(i => i.AddedToWatchAt ?? i.UpdateDate),
            _ => items.OrderByDescending(i => i.UpdateDate)
        };

        var totalCount = items.Count();



        var paged = items
            .OrderByDescending(i => i.AddedToWatchAt ?? i.WatchedAt)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(item => new WatchListItemDto(
                item.Id,
                item.WatchStatus.ConvertToApplicationWatchStatus(),
                item.AddedToWatchAt,
                item.WatchedAt,
                item.IsFavourite,
                new MovieWatchlistItemDto(
                    item.Movie.SimklId,
                    item.Movie.Title,
                    item.Movie.Year,
                    item.Movie.PosterUrl,
                    item.Movie.ReleaseDate,
                    item.Movie.RuntimeMinutes,
                    item.Movie.Director,
                    item.Movie.Overview
                )
            ))
            .ToList();

        return new PagedWatchListItemsDto(query.PageNumber, query.PageSize, totalCount, paged);
    }
}
