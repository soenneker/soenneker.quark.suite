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

    public static PresetDateRangePickerOption LastDays(int days, string? label = null, string? key = null)
    {
        if (days <= 0)
            throw new ArgumentOutOfRangeException(nameof(days), days, "Days must be greater than zero.");

        string resolvedLabel = label ?? $"Last {days.ToString(CultureInfo.InvariantCulture)} days";
        string resolvedKey = key ?? $"last-{days.ToString(CultureInfo.InvariantCulture)}-days";

        return new PresetDateRangePickerOption(resolvedKey, resolvedLabel, -days + 1, 0);
    }

    public DateOnly GetStart(DateOnly anchorDate) => anchorDate.AddDays(StartOffsetDays);

    public DateOnly GetEnd(DateOnly anchorDate) => anchorDate.AddDays(EndOffsetDays);
}
