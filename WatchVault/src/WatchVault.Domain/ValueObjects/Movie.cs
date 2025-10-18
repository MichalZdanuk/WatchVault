namespace WatchVault.Domain.ValueObjects;
/// <summary>
/// Snapshot of movie details taken from Simkl at the moment the user adds the movie.
/// </summary>
public record Movie
{
    public int SimklId { get; }
    public string Title { get; }
    public int Year { get; }
    public string PosterUrl { get; } = default!;
    public DateTime? ReleaseDate { get; }
    public int? RuntimeMinutes { get; }
    public string Director { get; } = default!;
    public string Overview { get; } = default!;
    public IReadOnlyList<string> Genres { get; } = new List<string>();

    protected Movie() { }

    private Movie(int simklId, string title, int year, string posterUrl,
        DateTime? releaseDate, int? runtimeMinutes, string director, string overview, IReadOnlyList<string> genres)
    {
        SimklId = simklId;
        Title = title;
        Year = year;
        PosterUrl = posterUrl;
        ReleaseDate = releaseDate;
        RuntimeMinutes = runtimeMinutes;
        Director = director;
        Overview = overview;
        Genres = genres;
    }

    public static Movie Of(int simklId, string title, int year, string type,
        string posterUrl, DateTime? releaseDate, int? runtimeMinutes,
        string director, string overview, IReadOnlyList<string> genres)
    {
        return new Movie(simklId, title.Trim(), year, posterUrl.Trim(),
            releaseDate, runtimeMinutes, director.Trim(), overview.Trim(), genres?.ToList() ?? new List<string>());
    }
}
