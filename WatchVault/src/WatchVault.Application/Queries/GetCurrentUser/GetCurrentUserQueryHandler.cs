using WatchVault.Application.Common;
using WatchVault.Application.Repositories;

namespace WatchVault.Application.Queries.GetCurrentUser;
public sealed class GetCurrentUserQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork)
    : IQueryHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var externalId = userContext.UserId;

        var user = await unitOfWork.UserRepository.GetByExternalIdAsync(externalId);

        if (user is null)
        {
            throw new UnauthorizedAccessException($"User {externalId} not found.");
        }

        return new UserDto(
            user.Id,
            user.ExternalId,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email
        );
    }
}
