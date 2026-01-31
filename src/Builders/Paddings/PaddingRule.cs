using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single padding rule with optional BreakpointType.
/// </summary>
internal record PaddingRule(string Size, ElementSideType Side, BreakpointType? Breakpoint = null);

