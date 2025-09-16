namespace WatchVault.Application.Commands.Login;
public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginDto>
{
    public Task<LoginDto> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
