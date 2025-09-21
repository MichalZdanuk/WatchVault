using WatchVault.Application.Common;
using WatchVault.Application.Repositories;
using WatchVault.Domain.Entities;

namespace WatchVault.Application.Queries.GetWatchList;
public sealed class GetWatchListQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetWatchListQuery, WatchListDto>
{
    public async Task<WatchListDto> Handle(GetWatchListQuery query, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var watchList = await unitOfWork.WatchListRepository.GetByUserIdAsync(userId);

        if (watchList is null)
        {
            throw new Exception($"User {userContext.UserId} has no watch list.");
        }

        var dto = RetrieveDto(watchList);

        return dto;
    }

    public WatchListDto RetrieveDto(WatchList watchList)
    {
        var dto = new WatchListDto(
            watchList.Id,
            watchList.Items.Count(x => x.WatchStatus == Domain.Enums.WatchStatus.Watched),
            watchList.Items.Count(x => x.WatchStatus == Domain.Enums.WatchStatus.ToWatch),
            watchList.Items
                .Select(item => new WatchListItemDto(
                    item.Id,
                    item.WatchStatus,
                    item.AddedToWatchAt,
                    item.WatchedAt,
                    new SimklMovieDto(
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
                .ToList()
        );

        return dto;
    }
}
