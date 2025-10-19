using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single border opacity rule with optional BreakpointType.
/// </summary>
internal record BorderOpacityRule(int Value, BreakpointType? breakpoint = null);




