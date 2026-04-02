namespace Soenneker.Quark;

/// <summary>
/// Represents a single stroke line join rule with optional BreakpointType.
/// </summary>
internal record StrokeLineJoinRule(string Value, BreakpointType? Breakpoint = null);
