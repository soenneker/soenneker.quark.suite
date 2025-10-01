using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single position rule with optional BreakpointType.
/// </summary>
internal record PositionRule(string Position, BreakpointType? breakpoint = null);

