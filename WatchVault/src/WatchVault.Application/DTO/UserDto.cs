namespace WatchVault.Application.DTO;
public record UserDto(Guid Id, Guid ExternalId, string FirstName, string LastName, string UserName, string Email);
