namespace WatchVault.Application.Queries.GetCurrentUser;
public sealed class GetCurrentUserQueryHandler() : IQueryHandler<GetCurrentUserQuery, UserDto>
{
    public Task<UserDto> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
