using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Options;

namespace WatchVault.Application.Services;
public class KeycloakService : IUserRegistrationService
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _options;

    public KeycloakService(HttpClient httpClient, IOptions<KeycloakOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> CreateUserAsync(string firstName, string lastName,
        string username, string email, string password)
    {
        var token = await GetAdminTokenAsync();

        var user = new
        {
            username,
            email,
            firstName,
            lastName,
            enabled = true,
            credentials = new[]
            {
                new { type = "password", value = password, temporary = false }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_options.AuthServerUrl}/admin/realms/{_options.Realm}/users")
        {
            Content = JsonContent.Create(user)
        };

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var resp = await _httpClient.SendAsync(request);

        await VerifyIfConflictedResponse(resp);

        resp.EnsureSuccessStatusCode();

        var location = resp.Headers.Location?.ToString()
            ?? throw new InvalidOperationException("User creation response does not contain Location header.");

        return location.Split('/').Last();
    }

    public async Task<string> RetrieveTokenAsync(string username, string password)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", _options.GrantType),
            new KeyValuePair<string, string>("client_id", _options.ClientId),
            new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

        var resp = await _httpClient.PostAsync(
            $"{_options.AuthServerUrl}/realms/{_options.Realm}/protocol/openid-connect/token",
            content);

        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadFromJsonAsync<JsonElement>();
        if (!json.TryGetProperty("access_token", out var accessToken))
            throw new InvalidOperationException("Keycloak token response does not contain access_token.");

        return accessToken.GetString()!;
    }

    private async Task<string> GetAdminTokenAsync()
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _options.ClientId),
            new KeyValuePair<string, string>("client_secret", _options.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var resp = await _httpClient.PostAsync(
            $"{_options.AuthServerUrl}/realms/{_options.Realm}/protocol/openid-connect/token",
            content);

        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadFromJsonAsync<JsonElement>();

        if (!json.TryGetProperty("access_token", out var accessToken))
            throw new InvalidOperationException("Keycloak token response does not contain access_token.");

        return accessToken.GetString()!;
    }

    private async Task VerifyIfConflictedResponse(HttpResponseMessage resp)
    {
        if (resp.StatusCode == HttpStatusCode.Conflict)
        {
            var content = await resp.Content.ReadAsStringAsync();

            try
            {
                var json = JsonDocument.Parse(content);
                if (json.RootElement.TryGetProperty("errorMessage", out var messageProp))
                {
                    var message = messageProp.GetString() ?? "Username or email already exists.";
                    throw new UserCredentialsAlreadyTakenException(message);
                }
            }
            catch (JsonException)
            {
                throw new UserCredentialsAlreadyTakenException("Conflict occurred while creating user.");
            }
        }
    }
}
