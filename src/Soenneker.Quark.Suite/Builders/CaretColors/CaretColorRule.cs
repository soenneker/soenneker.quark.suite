namespace Soenneker.Quark;

/// <summary>
/// Represents a single caret-color rule with optional BreakpointType.
/// </summary>
internal record CaretColorRule(string Value, BreakpointType? Breakpoint = null);
