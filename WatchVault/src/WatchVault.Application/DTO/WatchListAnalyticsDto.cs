namespace WatchVault.Application.DTO;
public record WatchListAnalyticsDto(string Period, IReadOnlyList<WatchListAnalyticsRecordDto> Data);

public record WatchListAnalyticsRecordDto(string PeriodLabel, int WatchedCount, int TotalRuntimeMinutes);
