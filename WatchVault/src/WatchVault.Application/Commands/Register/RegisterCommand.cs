namespace WatchVault.Application.Commands.Register;
public record RegisterCommand(string Username,
    string Email,
    string Password) : ICommand;
