namespace WatchVault.Application.ValueObjects;
public record OmdbSearchResponse(List<OmdbMovie> Search, string totalResults, string Response);
