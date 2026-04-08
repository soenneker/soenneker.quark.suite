using System;

namespace Soenneker.Quark;

/// <summary>
/// Represents a selected start and end date for the <see cref="Calendar"/> range mode.
/// </summary>
public sealed record CalendarDateRange(DateOnly? From, DateOnly? To);
