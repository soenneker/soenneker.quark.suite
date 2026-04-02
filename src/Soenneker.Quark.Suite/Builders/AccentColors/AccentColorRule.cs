namespace Soenneker.Quark;

/// <summary>
/// Represents a single accent-color rule with optional BreakpointType.
/// </summary>
internal record AccentColorRule(string Value, BreakpointType? Breakpoint = null);
