namespace WatchVault.Application.Options;
public class KeycloakOptions
{
    public required string AuthServerUrl { get; init; }
    public required string Realm { get; init; }
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string GrantType { get; init; }
}
