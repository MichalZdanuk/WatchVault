using WatchVault.Shared.DDD;

namespace WatchVault.Domain.Entities;
public class User : Entity
{
    public Guid ExternalId { get; private set; }

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    private User() { }

    public static User Create(Guid externalId,
        string firstName, string lastName, string userName, string email)
    {
        return new User()
        {
            ExternalId = externalId,
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email
        };
    }
}