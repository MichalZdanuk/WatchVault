namespace WatchVault.API.Options;

public class CorsPolicyOptions
{
    public string PolicyName { get; set; } = "CorsPolicy";
    public string[] AllowedOrigins { get; set; } = [];
}
