using System;
namespace Soenneker.Quark;
/// <summary>An event with absolute instants. An omitted end marks a point in time. A supplied end is exclusive; the scheduler converts instants into its display timezone.</summary>
public sealed record SchedulerEvent(string Id, string Title, DateTimeOffset Start, DateTimeOffset? End = null, string? ResourceId = null)
{
    internal bool Intersects(DateTimeOffset start, DateTimeOffset end) =>
        Start < end && (End is { } finish ? finish > start : Start >= start);
}
