using System;

namespace Soenneker.Quark;

internal readonly record struct EventTimelineEntry(EventTimelineItem Item, int SourceIndex)
{
    internal static readonly Comparison<EventTimelineEntry> OldestFirst = static (left, right) =>
    {
        int comparison = left.Item.When.CompareTo(right.Item.When);
        return comparison != 0 ? comparison : left.SourceIndex.CompareTo(right.SourceIndex);
    };

    internal static readonly Comparison<EventTimelineEntry> NewestFirst = static (left, right) =>
    {
        int comparison = right.Item.When.CompareTo(left.Item.When);
        return comparison != 0 ? comparison : left.SourceIndex.CompareTo(right.SourceIndex);
    };
}
