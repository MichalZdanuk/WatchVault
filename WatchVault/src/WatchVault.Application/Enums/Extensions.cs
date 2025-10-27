namespace WatchVault.Application.Enums;
public static class Extensions
{
    public static Domain.Enums.WatchStatus ConvertToDomainWatchStatus(this WatchStatus status)
    {
        if (status == WatchStatus.ToWatch)
        {
            return Domain.Enums.WatchStatus.ToWatch;
        }
        else
        {
            return Domain.Enums.WatchStatus.Watched;
        }
    }

    public static WatchStatus ConvertToApplicationWatchStatus(this Domain.Enums.WatchStatus watchStatus)
    {
        if (watchStatus == Domain.Enums.WatchStatus.ToWatch)
        {
            return WatchStatus.ToWatch;
        }
        else
        {
            return WatchStatus.Watched;
        }
    }
}
