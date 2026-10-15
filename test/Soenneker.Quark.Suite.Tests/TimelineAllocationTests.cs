using System;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class TimelineAllocationTests
{
    [Test]
    public void Sorting_existing_timeline_items_reuses_buffers()
    {
        var timeline = new EventTimelinePreparationProbe(
            [new() { Label = "Later", When = DateTimeOffset.UnixEpoch.AddHours(1) },
                new() { Label = "Earlier", When = DateTimeOffset.UnixEpoch },
                new() { Label = "Also earlier", When = DateTimeOffset.UnixEpoch }]);
        for (int i = 0; i < 100; i++)
        {
            timeline.ReverseSort();
        }

        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 100; i++)
        {
            timeline.ReverseSort();
        }
        (GC.GetAllocatedBytesForCurrentThread() - before).Should().Be(0);
    }
}
