using System;

namespace Soenneker.Quark;

/// <summary>A selected start instant and optional resource lane.</summary>
public sealed record SchedulerSlot(DateTimeOffset Start, string? ResourceId);
