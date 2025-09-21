namespace WatchVault.Application.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password) : ICommand<Guid>;
