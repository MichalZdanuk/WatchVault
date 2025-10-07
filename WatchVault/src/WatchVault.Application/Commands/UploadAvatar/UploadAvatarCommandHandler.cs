using WatchVault.Application.Common;
using WatchVault.Application.Repositories;
using WatchVault.Application.Services;

namespace WatchVault.Application.Commands.UploadAvatar;
public sealed class UploadAvatarCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,
    IBlobService blobService)
    : ICommandHandler<UploadAvatarCommand, bool>
{
    public async Task<bool> Handle(UploadAvatarCommand command, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new UnauthorizedAccessException($"User {userId} not found.");
        }

        if (user.ImageId != Guid.Empty)
        {
            await blobService.DeleteAsync(user.ImageId);
        }

        var newImageId = Guid.NewGuid();
        using var stream = command.File.OpenReadStream();
        await blobService.UploadAsync(stream, newImageId, command.File.ContentType);

        user.UpdateImage(newImageId);

        await unitOfWork.UserRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
