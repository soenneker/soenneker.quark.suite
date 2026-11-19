using System;

namespace Soenneker.Quark;

/// <summary>A selected start instant and optional resource lane.</summary>
public sealed record SchedulerSlot(DateTimeOffset Start, string? ResourceId)
{
    /// <summary>The exclusive end instant for a booking selection. Calendar management selections specify only a start.</summary>
    public DateTimeOffset? End { get; init; }
}
