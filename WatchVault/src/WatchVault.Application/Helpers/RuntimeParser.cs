using System.Text.RegularExpressions;

namespace WatchVault.Application.Helpers;
public static class RuntimeParser
{
    private static readonly Dictionary<char, int> TimeMultipliers = new()
        {
            { 'h', 60 },
            { 'm', 1 }
        };

    /// <summary>
    /// Converts a runtime string like "1h 55m" or "2h" or "45m" into total minutes.
    /// Returns 0 if input is null, empty, or invalid.
    /// </summary>
    public static int GetRuntimeMinutes(string? runtime)
    {
        if (string.IsNullOrWhiteSpace(runtime))
            return 0;

        runtime = runtime.Trim().ToLowerInvariant();

        int totalMinutes = 0;

        var matches = Regex.Matches(runtime, @"(\d+)\s*([hm])");

        foreach (Match match in matches)
        {
            if (int.TryParse(match.Groups[1].Value, out int value) &&
                TimeMultipliers.TryGetValue(match.Groups[2].Value[0], out int multiplier))
            {
                totalMinutes += value * multiplier;
            }
        }

        return totalMinutes;
    }
}
