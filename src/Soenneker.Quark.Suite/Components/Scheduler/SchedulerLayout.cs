using System;
using System.Collections.Generic;
using System.Linq;
namespace Soenneker.Quark;
/// <summary>Assigns overlapping events to columns without changing their absolute instants.</summary>
public static class SchedulerLayout
{
    public static IReadOnlyList<SchedulerEventPlacement> Arrange(IEnumerable<SchedulerEvent> events, DateOnly day, TimeZoneInfo zone)
    {
        var midnight = day.ToDateTime(TimeOnly.MinValue);
        var placements = events.Select(e => new SchedulerEventPlacement(e,
            Math.Clamp((TimeZoneInfo.ConvertTime(e.Start, zone).DateTime - midnight).TotalMinutes, 0, 1440),
            Math.Clamp((TimeZoneInfo.ConvertTime(e.End, zone).DateTime - midnight).TotalMinutes, 0, 1440), 0, 1))
            .Where(p => p.Event.End > p.Event.Start).OrderBy(p => p.StartMinute).ThenByDescending(p => p.EndMinute).ToArray();
        var result = new List<SchedulerEventPlacement>();
        var group = new List<SchedulerEventPlacement>();
        var active = new PriorityQueue<int, double>();
        var available = new PriorityQueue<int, int>();
        int columns = 0;
        double groupEnd = -1;
        void Flush() { foreach (var item in group) result.Add(item with { ColumnCount = columns }); group.Clear(); active.Clear(); available.Clear(); columns = 0; groupEnd = -1; }
        foreach (var placement in placements)
        {
            if (placement.StartMinute >= groupEnd) Flush();
            // Preserve a visible hit target across DST folds and for very short events.
            var end = Math.Max(placement.StartMinute + 30, placement.EndMinute);
            while (active.TryPeek(out var released, out var finish) && finish <= placement.StartMinute)
            {
                active.Dequeue();
                available.Enqueue(released, released);
            }
            var column = available.Count == 0 ? columns++ : available.Dequeue();
            active.Enqueue(column, end);
            group.Add(placement with { Column = column, EndMinute = end });
            groupEnd = Math.Max(groupEnd, end);
        }
        Flush(); return result;
    }
}
