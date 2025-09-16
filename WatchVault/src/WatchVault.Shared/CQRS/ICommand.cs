namespace WatchVault.Shared.CQRS;
public interface ICommand : ICommand<Unit>, IRequest { }
public interface ICommand<TRespone> : IRequest<TRespone> { }
