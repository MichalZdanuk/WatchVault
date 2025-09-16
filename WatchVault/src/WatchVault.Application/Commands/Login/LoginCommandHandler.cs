namespace WatchVault.Application.Commands.Login;
public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    public Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
