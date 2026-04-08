using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Exposes per-day state for calendar day content customization.
/// </summary>
public sealed record CalendarDayContext(
    DateOnly Date,
    bool InMonth,
    bool Disabled,
    bool IsToday,
    bool IsSelected,
    bool IsRangeStart,
    bool IsRangeEnd,
    bool IsRangeMiddle,
    IReadOnlyList<string> Modifiers);
