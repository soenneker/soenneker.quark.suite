using System;
namespace Soenneker.Quark;
/// <summary>An event with absolute instants. End is exclusive; the scheduler converts instants into its display timezone.</summary>
public sealed record SchedulerEvent(string Id, string Title, DateTimeOffset Start, DateTimeOffset End, string? ResourceId = null);
