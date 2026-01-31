using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single border radius rule with optional BreakpointType.
/// </summary>
internal record BorderRadiusRule(string Size, ElementSideType Side, BreakpointType? Breakpoint = null, string CornerToken = "");
