namespace Soenneker.Quark;

/// <summary>
/// Represents a single scroll-snap-stop rule with optional BreakpointType.
/// </summary>
internal record ScrollSnapStopRule(string Value, BreakpointType? Breakpoint = null);
