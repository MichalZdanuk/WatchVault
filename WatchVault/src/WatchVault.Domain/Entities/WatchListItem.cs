using WatchVault.Domain.Enums;
using WatchVault.Domain.ValueObjects;
using WatchVault.Shared.DDD;

namespace WatchVault.Domain.Entities;
public class WatchListItem : Entity
{
    public Guid WatchListId { get; private set; }
    public WatchStatus WatchStatus { get; private set; }
    public DateTime? AddedToWatchAt { get; private set; }
    public DateTime? WatchedAt { get; private set; }
    public bool IsFavourite { get; private set; }
    public Movie Movie { get; private set; } = default!;

    private WatchListItem() { }

    private WatchListItem(Guid watchListId, WatchStatus watchStatus, Movie movie)
    {
        WatchListId = watchListId;
        WatchStatus = watchStatus;
        Movie = movie;
        if (watchStatus == WatchStatus.Watched)
        {
            WatchedAt = DateTime.UtcNow;
        }
        else if (watchStatus == WatchStatus.ToWatch)
        {
            AddedToWatchAt = DateTime.UtcNow;
        }
    }

    public static WatchListItem Create(Guid watchListId, WatchStatus status, Movie movie) =>
        new WatchListItem(watchListId, status, movie);

    public void MarkAsWatched()
    {
        if (WatchStatus == WatchStatus.Watched)
        {
            throw new InvalidOperationException("Movie already marked as watched.");
        }

        WatchStatus = WatchStatus.Watched;
        WatchedAt = DateTime.UtcNow;
    }

    public void ToggleFavourite()
    {
        if (WatchStatus != WatchStatus.Watched)
        {
            throw new InvalidOperationException("Only watched movies can be marked as favourite.");
        }

        IsFavourite = !IsFavourite;
    }
}
