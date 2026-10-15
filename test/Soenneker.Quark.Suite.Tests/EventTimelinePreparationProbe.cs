namespace Soenneker.Quark.Suite.Tests;

internal sealed class EventTimelinePreparationProbe : EventTimeline
{
    internal EventTimelinePreparationProbe(EventTimelineItem[] items) => Items = items;

    internal void ReverseSort()
    {
        NewestFirst = !NewestFirst;
        OnParametersSet();
    }
}
