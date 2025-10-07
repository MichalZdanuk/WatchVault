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
    public int TotalFavourites => _items.Count(x => x.IsFavourite);
    public int TotalRuntimeMinutes => _items
        .Where(x => x.WatchStatus == WatchStatus.Watched)
        .Sum(x => x.Movie.RuntimeMinutes ?? 0);

    public double AverageRuntimeMinutes
    {
        get
        {
            var runtimes = _items
                .Where(x => x.WatchStatus == WatchStatus.Watched && x.Movie.RuntimeMinutes.HasValue)
                .Select(x => x.Movie.RuntimeMinutes!.Value)
                .ToList();

            return runtimes.Count > 0 ? runtimes.Average() : 0;
        }
    }

    public int? EarliestYearWatched =>
        _items.Where(x => x.WatchStatus == WatchStatus.Watched).Select(x => (int?)x.Movie.Year).Min();
    public int? LatestYearWatched =>
        _items.Where(x => x.WatchStatus == WatchStatus.Watched).Select(x => (int?)x.Movie.Year).Max();

    public DateTime? LastWatchedAt =>
    _items.Where(x => x.WatchStatus == WatchStatus.Watched && x.WatchedAt.HasValue)
          .Max(x => x.WatchedAt);

    public DateTime? LastAddedToWatchAt =>
        _items.Where(x => x.WatchStatus == WatchStatus.ToWatch && x.AddedToWatchAt.HasValue)
              .Max(x => x.AddedToWatchAt);

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
