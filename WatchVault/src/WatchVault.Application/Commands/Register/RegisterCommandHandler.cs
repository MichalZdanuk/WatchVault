using WatchVault.Application.Repositories;
using WatchVault.Application.Services;
using WatchVault.Domain.Entities;

namespace WatchVault.Application.Commands.Register;
public sealed class RegisterCommandHandler(IUnitOfWork unitOfWork,
    IUserRegistrationService userRegistrationService)
    : ICommandHandler<RegisterCommand>
{
    public async Task<Unit> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userId = await userRegistrationService.CreateUserAsync(command.FirstName, command.LastName,
            command.Username, command.Email, command.Password);

        var user = User.Create(Guid.Parse(userId), command.FirstName, command.LastName, command.Username, command.Email);

        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
