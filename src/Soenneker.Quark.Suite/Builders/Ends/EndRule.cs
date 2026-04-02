namespace Soenneker.Quark;

/// <summary>
/// Represents a single end (inset-inline-end) rule with optional BreakpointType.
/// </summary>
internal record EndRule(string Value, BreakpointType? Breakpoint = null);
