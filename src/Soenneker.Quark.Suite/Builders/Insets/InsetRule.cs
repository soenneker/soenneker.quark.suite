namespace Soenneker.Quark;

/// <summary>
/// Represents a single inset rule with optional BreakpointType.
/// </summary>
internal record InsetRule(string Size, ElementSideType Side, BreakpointType? Breakpoint = null);
