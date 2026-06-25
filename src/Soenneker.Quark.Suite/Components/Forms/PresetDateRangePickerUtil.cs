using System;

namespace Soenneker.Quark;

internal static class PresetDateRangePickerUtil
{
    internal static DateOnly GetSelectableMin(DateOnly min, DateOnly max, int? maxRangeDays, bool disableDatesOutsideMaxRangeWindow)
    {
        if (min > max)
            (min, max) = (max, min);

        if (!disableDatesOutsideMaxRangeWindow || maxRangeDays is null)
            return min;

        DateOnly rangeMin = max.AddDays(-Math.Max(1, maxRangeDays.Value) + 1);
        return rangeMin > min ? rangeMin : min;
    }

    internal static (DateOnly Start, DateOnly End)? Normalize(CalendarDateRange? range, DateOnly min, DateOnly max, int? maxRangeDays,
        bool disableDatesOutsideMaxRangeWindow)
    {
        if (range is null)
            return null;

        DateOnly? selectedStart = range.From ?? range.To;
        DateOnly? selectedEnd = range.To ?? range.From;

        if (selectedStart is null || selectedEnd is null)
            return null;

        return Normalize(selectedStart.Value, selectedEnd.Value, min, max, maxRangeDays, disableDatesOutsideMaxRangeWindow);
    }

    internal static (DateOnly Start, DateOnly End) Normalize(DateOnly start, DateOnly end, DateOnly min, DateOnly max, int? maxRangeDays,
        bool disableDatesOutsideMaxRangeWindow)
    {
        if (min > max)
            (min, max) = (max, min);

        min = GetSelectableMin(min, max, maxRangeDays, disableDatesOutsideMaxRangeWindow);

        if (start > end)
            (start, end) = (end, start);

        start = Clamp(start, min, max);
        end = Clamp(end, min, max);

        if (start > end)
            (start, end) = (end, start);

        if (maxRangeDays is null)
            return (start, end);

        int effectiveMaxRangeDays = Math.Max(1, maxRangeDays.Value);

        if (end.DayNumber - start.DayNumber + 1 <= effectiveMaxRangeDays)
            return (start, end);

        DateOnly clampedEnd = start.AddDays(effectiveMaxRangeDays - 1);

        if (clampedEnd <= max)
            return (start, clampedEnd);

        DateOnly clampedStart = max.AddDays(-effectiveMaxRangeDays + 1);
        return (Clamp(clampedStart, min, max), max);
    }

    internal static DateOnly Clamp(DateOnly value, DateOnly min, DateOnly max)
    {
        if (value < min)
            return min;

        return value > max ? max : value;
    }
}
