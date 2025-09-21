namespace WatchVault.Application.Queries.RetrieveToken;
public record RetrieveToken(string Username, string Password) : ICommand<RetrieveTokenDto>;
