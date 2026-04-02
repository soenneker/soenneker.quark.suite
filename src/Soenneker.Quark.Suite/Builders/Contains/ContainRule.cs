namespace Soenneker.Quark;

/// <summary>
/// Represents a single contain rule with optional BreakpointType.
/// </summary>
internal record ContainRule(string Value, BreakpointType? Breakpoint = null);
