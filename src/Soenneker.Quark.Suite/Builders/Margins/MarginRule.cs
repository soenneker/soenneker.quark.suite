using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single margin rule with optional BreakpointType.
/// </summary>
internal record MarginRule(string Size, ElementSideType Side, BreakpointType? Breakpoint = null);
