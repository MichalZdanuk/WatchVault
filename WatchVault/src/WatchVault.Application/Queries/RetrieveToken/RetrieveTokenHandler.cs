using WatchVault.Application.Services;

namespace WatchVault.Application.Queries.RetrieveToken;
public sealed class RetrieveTokenHandler(IUserRegistrationService userRegistrationService)
    : ICommandHandler<RetrieveToken, RetrieveTokenDto>
{
    public async Task<RetrieveTokenDto> Handle(RetrieveToken command, CancellationToken cancellationToken)
    {
        var token = await userRegistrationService.RetrieveTokenAsync(command.Username, command.Password);

        return new RetrieveTokenDto(token);
    }
}
