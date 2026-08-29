using System;
using System.Collections.Generic;
using System.Globalization;

namespace Soenneker.Quark;

/// <summary>
/// Defines a relative preset option for <see cref="PresetDateRangePicker"/>.
/// </summary>
public sealed record PresetDateRangePickerOption(string Key, string Label, int StartOffsetDays, int EndOffsetDays)
{
    public static IReadOnlyList<PresetDateRangePickerOption> Defaults { get; } =
    [
        new("today", "Today", 0, 0),
        new("yesterday", "Yesterday", -1, -1),
        LastDays(3),
        LastDays(7),
        LastDays(15),
        LastDays(30)
    ];

    /// <summary>
    /// Returns the value produced by last Days.
    /// </summary>
    /// <param name="days">Days for the last days operation.</param>
    /// <param name="label">Human-readable label to display.</param>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    public static PresetDateRangePickerOption LastDays(int days, string? label = null, string? key = null)
    {
        if (days <= 0)
            throw new ArgumentOutOfRangeException(nameof(days), days, "Days must be greater than zero.");

        var resolvedLabel = label ?? $"Last {days.ToString(CultureInfo.InvariantCulture)} days";
        var resolvedKey = key ?? $"last-{days.ToString(CultureInfo.InvariantCulture)}-days";

        return new PresetDateRangePickerOption(resolvedKey, resolvedLabel, -days + 1, 0);
    }

    /// <summary>
    /// Retrieves start.
    /// </summary>
    /// <param name="anchorDate">Anchor Date for the get start operation.</param>
    /// <returns>The requested date Only.</returns>
    public DateOnly GetStart(DateOnly anchorDate) => anchorDate.AddDays(StartOffsetDays);

    /// <summary>
    /// Retrieves end.
    /// </summary>
    /// <param name="anchorDate">Anchor Date for the get end operation.</param>
    /// <returns>The requested date Only.</returns>
    public DateOnly GetEnd(DateOnly anchorDate) => anchorDate.AddDays(EndOffsetDays);
}
