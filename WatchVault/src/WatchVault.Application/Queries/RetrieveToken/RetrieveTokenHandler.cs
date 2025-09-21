using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.RetrieveToken;
public sealed class RetrieveTokenHandler(IUserRegistrationService userRegistrationService)
    : IQueryHandler<RetrieveToken, RetrieveTokenDto>
{
    public async Task<RetrieveTokenDto> Handle(RetrieveToken query, CancellationToken cancellationToken)
    {
        var token = await userRegistrationService.RetrieveTokenAsync(query.Username, query.Password);

        return new RetrieveTokenDto(token);
    }
}
