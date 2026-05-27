using System;

namespace Soenneker.Quark;

/// <summary>
/// Resolves component time zone identifiers with graceful fallbacks.
/// </summary>
public static class QuarkDateTimeZoneResolver
{
    /// <summary>
    /// Resolves a component time zone, falling back to the browser time zone and then the local system time zone.
    /// </summary>
    /// <param name="timeZone">The requested component time zone identifier.</param>
    /// <param name="browserTimeZone">The detected browser time zone identifier.</param>
    /// <returns>A valid time zone.</returns>
    public static TimeZoneInfo Resolve(string? timeZone, string? browserTimeZone)
    {
        if (TryResolve(timeZone, out var resolved))
            return resolved;

        if (TryResolve(browserTimeZone, out resolved))
            return resolved;

        return TimeZoneInfo.Local;
    }

    /// <summary>
    /// Attempts to resolve a time zone identifier.
    /// </summary>
    /// <param name="timeZone">The time zone identifier.</param>
    /// <param name="resolved">The resolved time zone when successful.</param>
    /// <returns><c>true</c> when the identifier is valid; otherwise, <c>false</c>.</returns>
    public static bool TryResolve(string? timeZone, out TimeZoneInfo resolved)
    {
        resolved = TimeZoneInfo.Local;

        if (string.IsNullOrWhiteSpace(timeZone))
            return false;

        try
        {
            resolved = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return true;
        }
        catch (TimeZoneNotFoundException)
        {
            return false;
        }
        catch (InvalidTimeZoneException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
