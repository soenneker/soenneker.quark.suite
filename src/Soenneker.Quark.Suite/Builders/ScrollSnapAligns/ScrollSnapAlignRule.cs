namespace Soenneker.Quark;

/// <summary>
/// Represents a single scroll-snap-align rule with optional BreakpointType.
/// </summary>
internal record ScrollSnapAlignRule(string Value, BreakpointType? Breakpoint = null);
