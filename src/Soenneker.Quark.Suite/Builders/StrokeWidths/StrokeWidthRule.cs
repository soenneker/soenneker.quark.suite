namespace Soenneker.Quark;

/// <summary>
/// Represents a single stroke width rule with optional BreakpointType.
/// </summary>
internal record StrokeWidthRule(string Value, BreakpointType? Breakpoint = null);
