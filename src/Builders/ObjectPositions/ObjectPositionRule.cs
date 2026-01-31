using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single object position rule with optional BreakpointType.
/// </summary>
internal record ObjectPositionRule(string Position, BreakpointType? Breakpoint = null);
