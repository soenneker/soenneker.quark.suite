using System;
using System.Globalization;
namespace Soenneker.Quark;
/// <summary>Timezone-aware calendar calculations. Invalid DST wall times are rejected; ambiguous times use the earlier instant.</summary>
public static class SchedulerDates
{
    public static DateTimeOffset? ToInstant(DateTime wallTime, TimeZoneInfo zone)
    {
        var local = DateTime.SpecifyKind(wallTime, DateTimeKind.Unspecified);
        if (zone.IsInvalidTime(local)) return null;
        var offset = zone.GetUtcOffset(local);
        if (zone.IsAmbiguousTime(local)) { var offsets = zone.GetAmbiguousTimeOffsets(local); offset = offsets[0] > offsets[1] ? offsets[0] : offsets[1]; }
        return new DateTimeOffset(local, offset);
    }
    /// <summary>Preserves the original instant when a draft keeps its wall time, including the later occurrence of a DST overlap.</summary>
    public static DateTimeOffset? ResolveEdit(DateTime wallTime, TimeZoneInfo zone, DateTimeOffset? original)
    {
        if (original is { } instant && TimeZoneInfo.ConvertTime(instant, zone).DateTime == wallTime) return instant;
        return ToInstant(wallTime, zone);
    }
    public static DateOnly WeekStart(DateOnly date, DayOfWeek firstDay) => date.AddDays(-((7 + (int)date.DayOfWeek - (int)firstDay) % 7));
    public static DateOnly LocalDate(DateTimeOffset instant, TimeZoneInfo zone) => DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(instant, zone).DateTime);
    public static string InputValue(DateTimeOffset instant, TimeZoneInfo zone) => TimeZoneInfo.ConvertTime(instant, zone).ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
}
