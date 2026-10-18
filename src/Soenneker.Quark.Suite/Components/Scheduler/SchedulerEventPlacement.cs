namespace Soenneker.Quark;
/// <summary>Wall-clock placement within one day. Columns are shared only by connected overlapping events.</summary>
public sealed record SchedulerEventPlacement(SchedulerEvent Event, double StartMinute, double EndMinute, int Column, int ColumnCount);
