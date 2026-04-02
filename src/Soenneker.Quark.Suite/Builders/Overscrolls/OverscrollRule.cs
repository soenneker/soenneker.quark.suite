namespace Soenneker.Quark;

/// <summary>
/// Represents a single overscroll utility rule with an optional breakpoint.
/// </summary>
internal record OverscrollRule(string Value, BreakpointType? Breakpoint = null);
