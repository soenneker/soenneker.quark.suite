using Soenneker.Quark.Enums;



namespace Soenneker.Quark;

/// <summary>
/// Represents a single border rule with optional BreakpointType.
/// </summary>
internal record BorderRule(string Size, ElementSideType Side, BreakpointType? breakpoint = null);
