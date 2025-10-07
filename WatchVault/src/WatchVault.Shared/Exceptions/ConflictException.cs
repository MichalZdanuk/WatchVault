namespace WatchVault.Shared.Exceptions;
public abstract class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
    }
}