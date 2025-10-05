using WatchVault.Application.Repositories;
using WatchVault.Application.Services;
using WatchVault.Domain.Entities;

namespace WatchVault.Application.Commands.Register;
public sealed class RegisterCommandHandler(IUnitOfWork unitOfWork,
    IUserRegistrationService userRegistrationService,
    IBlobService blobService)
    : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var userId = await userRegistrationService.CreateUserAsync(command.FirstName, command.LastName,
            command.Username, command.Email, command.Password);

        var fileId = Guid.NewGuid();

        var user = User.Create(Guid.Parse(userId), command.FirstName, command.LastName, command.Username, command.Email, fileId);
        var watchList = WatchList.Create(user.Id);

        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.WatchListRepository.AddAsync(watchList);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        using var stream = command.File.OpenReadStream();
        await blobService.UploadAsync(stream, fileId, command.File.ContentType);

        return user.Id;
    }
}
