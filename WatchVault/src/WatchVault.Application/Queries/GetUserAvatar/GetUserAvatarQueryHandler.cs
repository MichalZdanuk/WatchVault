using WatchVault.Application.Common;
using WatchVault.Application.Exceptions;
using WatchVault.Application.Repositories;
using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.GetUserAvatar;
public sealed class GetUserAvatarQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,
    IBlobService blobService)
    : IQueryHandler<GetUserAvatarQuery, FileResponseDto>
{
    public async Task<FileResponseDto> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            throw new Exception($"User in context not exist");
        }

        var blob = await blobService.GetAsync(user.ImageId);

        if (blob is null)
        {
            throw new AvatarNotFoundException(userContext.UserId);
        }

        return new FileResponseDto(blob.Value.Stream, blob.Value.ContentType);
    }
}
