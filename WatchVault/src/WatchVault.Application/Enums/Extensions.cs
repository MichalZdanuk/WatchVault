using WatchVault.Domain.Enums;

namespace WatchVault.Application.Enums;
public static class Extensions
{
    public static WatchStatus ConvertToDomainWatchStatus(this Status status)
    {
        if (status == Status.ToWatch)
        {
            return WatchStatus.ToWatch;
        }
        else
        {
            return WatchStatus.Watched;
        }
    }
}
