using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using WatchVault.Application.Helpers;
using WatchVault.Application.Options;
using WatchVault.Application.ValueObjects;
using WatchVault.Shared.Pagination;

namespace WatchVault.Application.Services;
public class SimklApiConnector : ISimklApiConnector
{
    private readonly HttpClient _httpClient;
    private readonly SimklOptions _options;

    public SimklApiConnector(HttpClient httpClient, IOptions<SimklOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseAddress);
        _httpClient.DefaultRequestHeaders.Add("simkl-api-key", _options.ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<PagedResponse<SimklSearchMovie>> SearchMoviesAsync(string query, int page = 1, int limit = 10)
    {
        var url = $"search/movie?q={Uri.EscapeDataString(query)}&page={page}&limit={limit}";
        var raw = await _httpClient.GetFromJsonAsync<List<SearchMovieResponse>>(url);

        var items = raw?.Select(r => new SimklSearchMovie(
            r.Title,
            r.Year,
            r.EndpointType,
            string.IsNullOrEmpty(r.Poster) ? string.Empty : $"https://simkl.in/posters/{r.Poster}_m.jpg",
            r.Ids.SimklId
        )).ToList() ?? new();

        return new PagedResponse<SimklSearchMovie>(page, limit, items);
    }

    public async Task<SimklMovieDetails?> GetMovieDetailsAsync(int simklId)
    {
        var url = $"movies/{simklId}?extended=full";
        var raw = await _httpClient.GetFromJsonAsync<MovieDetailsResponse>(url);

        if (raw == null) return null;

        DateTime? releasedDate = null;
        if (!string.IsNullOrWhiteSpace(raw.Released) && DateTime.TryParse(raw.Released, out var parsed))
        {
            releasedDate = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
        }

        return new SimklMovieDetails(
            raw.Title,
            raw.Year,
            raw.Type,
            raw.Ids.Simkl,
            string.IsNullOrEmpty(raw.Poster) ? string.Empty : $"https://simkl.in/posters/{raw.Poster}_m.jpg",
            string.IsNullOrEmpty(raw.Fanart) ? string.Empty : $"https://simkl.in/fanart/{raw.Fanart}_mobile.jpg",
            releasedDate,
            raw.Runtime,
            raw.Ratings?.Imdb?.Rating,
            raw.Director,
            raw.Overview,
            raw.Genres ?? new List<string>(),
            raw.UserRecommendations?.Select(u =>
                new UserRecommendation(
                    u.Title,
                    u.Year,
                    string.IsNullOrEmpty(u.Poster) ? string.Empty : $"https://simkl.in/posters/{u.Poster}_m.jpg",
                    u.Ids.Simkl
                )).ToList() ?? new List<UserRecommendation>()
                );
    }

    public async Task<PagedResponse<SimklTrendingMovie>> GetTrendingMoviesAsync(string period = "month")
    {
        var url = $"movies/trending/{period}?extended=overview,theater,metadata";
        var raw = await _httpClient.GetFromJsonAsync<List<TrendingMovieResponse>>(url);

        var items = raw?.Select(r => new SimklTrendingMovie(
            r.Title,
            r.Url,
            string.IsNullOrEmpty(r.Poster) ? string.Empty : $"https://simkl.in/posters/{r.Poster}_m.jpg",
            r.Ids.SimklId,
            r.ReleaseDate,
            r.Ratings?.Simkl?.Rating,
            r.Runtime,
            RuntimeParser.GetRuntimeMinutes(r.Runtime),
            r.Overview,
            CleanMetadata(r.Metadata)
        )).ToList() ?? new();

        return new PagedResponse<SimklTrendingMovie>(1, items.Count, items);
    }

    private string CleanMetadata(string metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata))
        {
            return string.Empty;
        }

        var parts = metadata.Split('•');

        if (parts.Length > 1)
        {
            return string.Join(" • ", parts.Skip(1)).Trim();
        }

        return string.Empty;
    }

    private record SearchMovieResponse(string Title, int Year, string EndpointType, string Poster, SearchIds Ids);
    private record SearchIds(
        [property: JsonPropertyName("simkl_id")] int SimklId,
        [property: JsonPropertyName("slug")] string Slug,
        [property: JsonPropertyName("tmdb")] string? Tmdb
    );

    private record MovieDetailsResponse(
        string Title,
        int Year,
        string Type,
        MovieDetailsIds Ids,
        Ratings Ratings,
        string Poster,
        string Fanart,
        string Released,
        int? Runtime,
        string Director,
        string Overview,
        List<string> Genres,
        [property: JsonPropertyName("users_recommendations")] List<UserRecommendationResponse> UserRecommendations
    );
    private record MovieDetailsIds(
        [property: JsonPropertyName("simkl")] int Simkl,
        [property: JsonPropertyName("slug")] string Slug,
        [property: JsonPropertyName("tmdb")] string? Tmdb,
        [property: JsonPropertyName("imdb")] string? Imdb
    );

    private record UserRecommendationResponse(
        string Title,
        int? Year,
        string Poster,
        UserRecommendationIds Ids
    );

    private record UserRecommendationIds([property: JsonPropertyName("simkl")] int Simkl);

    private record TrendingMovieResponse(
        string Title,
        string Url,
        string Poster,
        TrendingIds Ids,
        [property: JsonPropertyName("release_date")] string ReleaseDate,
        Ratings Ratings,
        string Runtime,
        string Overview,
        string Metadata
    );
    private record TrendingIds(
        [property: JsonPropertyName("simkl_id")] int SimklId,
        [property: JsonPropertyName("slug")] string Slug
    );
    private record Ratings(RatingSource? Simkl, RatingSource? Imdb);
    private record RatingSource(double? Rating, int? Votes);
}
