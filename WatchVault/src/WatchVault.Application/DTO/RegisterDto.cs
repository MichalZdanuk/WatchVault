using Microsoft.AspNetCore.Http;

namespace WatchVault.Application.DTO;
public record RegisterDto(string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password,
    IFormFile File);
