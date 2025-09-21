namespace WatchVault.Application.ValueObjects;
public record SimklSearchMovie(
    string Title,
    int Year,
    string EndpointType,
    string Poster,
    int SimklId
);
