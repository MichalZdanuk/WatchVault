using WatchVault.Domain.Enums;
using WatchVault.Domain.ValueObjects;
using WatchVault.Shared.DDD;

namespace WatchVault.Domain.Entities;
public class WatchList : Aggregate
{
    public Guid UserId { get; private set; }
    private readonly List<WatchListItem> _items = new();
    public IReadOnlyCollection<WatchListItem> Items => _items.AsReadOnly();

    public int TotalWatched => _items.Count(x => x.WatchStatus == WatchStatus.Watched);
    public int TotalToWatch => _items.Count(x => x.WatchStatus == WatchStatus.ToWatch);

    private WatchList() { }

    public static WatchList Create(Guid userId) =>
        new WatchList { Id = Guid.NewGuid(), UserId = userId };

    public void AddToWatch(Movie movie)
    {
        var item = _items.FirstOrDefault(x => x.Movie.SimklId == movie.SimklId);
        if (item is null)
        {
            var newItem = WatchListItem.Create(Id, WatchStatus.ToWatch, movie);
            _items.Add(newItem);
            return;
        }
        else
        {
            throw new InvalidOperationException($"Movie {movie.SimklId} already exists.");
        }
    }

    public void AddWatched(Movie movie)
    {
        var item = _items.FirstOrDefault(x => x.Movie.SimklId == movie.SimklId);
        if (item is null)
        {
            var newItem = WatchListItem.Create(Id, WatchStatus.Watched, movie);
            _items.Add(newItem);
            return;
        }

        if (item.WatchStatus == WatchStatus.ToWatch)
        {
            item.MarkAsWatched();
            return;
        }
    }

    public void RemoveWatchListItem(Guid watchListItemId)
    {
        var item = GetItemOrThrow(watchListItemId);

        _items.Remove(item);
    }

    private WatchListItem GetItemOrThrow(Guid watchListItemId)
    {
        return _items.SingleOrDefault(x => x.Id == watchListItemId)
            ?? throw new InvalidOperationException($"WatchListItem {watchListItemId} was not found.");
    }
}
