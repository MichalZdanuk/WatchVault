using Microsoft.AspNetCore.Http;

namespace WatchVault.Application.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password,
    IFormFile File) : ICommand<Guid>;
