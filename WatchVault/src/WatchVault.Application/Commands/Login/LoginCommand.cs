namespace WatchVault.Application.Commands.Login;
public record LoginCommand(string Email, string Password) : ICommand<string>;
