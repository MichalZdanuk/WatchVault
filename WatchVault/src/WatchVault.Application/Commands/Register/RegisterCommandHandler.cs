namespace WatchVault.Application.Commands.Register;
public sealed class RegisterCommandHandler() : ICommandHandler<RegisterCommand>
{
    public Task<Unit> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
