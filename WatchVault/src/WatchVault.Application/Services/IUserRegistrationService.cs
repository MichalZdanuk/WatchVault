namespace WatchVault.Application.Services;
public interface IUserRegistrationService
{
    Task<string> CreateUserAsync(string firstName, string lastName, string username, string email, string password);
    Task<string> RetrieveTokenAsync(string username, string password);
}
