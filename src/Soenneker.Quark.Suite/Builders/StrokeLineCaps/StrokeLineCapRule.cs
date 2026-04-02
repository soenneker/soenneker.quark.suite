namespace Soenneker.Quark;

/// <summary>
/// Represents a single stroke line cap rule with optional BreakpointType.
/// </summary>
internal record StrokeLineCapRule(string Value, BreakpointType? Breakpoint = null);
