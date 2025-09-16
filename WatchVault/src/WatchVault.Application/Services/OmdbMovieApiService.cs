using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WatchVault.Application.ValueObjects;

namespace WatchVault.Application.Services;
public class OmdbMovieApiService : IMovieApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiPath = "http://www.omdbapi.com";

    public OmdbMovieApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Omdb:ApiKey"] ?? throw new InvalidOperationException("OMDb API key is not configured.");
    }

    public async Task<OmdbSearchResponse> SearchMoviesAsync(string searchTerm, int page = 1, CancellationToken cancellationToken = default)
    {
        var url = $"{_apiPath}/?s={Uri.EscapeDataString(searchTerm)}&page={page}&apikey={_apiKey}&plot=short";

        var response = await _httpClient.GetFromJsonAsync<OmdbSearchResponse>(url, cancellationToken);

        return response ?? new OmdbSearchResponse(new List<OmdbMovie>(), "0", "False");
    }
}
