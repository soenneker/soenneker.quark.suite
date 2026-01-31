using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single width rule with optional BreakpointType.
/// </summary>
internal record WidthRule(string Size, BreakpointType? Breakpoint = null);

