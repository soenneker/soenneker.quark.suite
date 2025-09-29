using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ObjectPositions;

/// <summary>
/// Represents a single object position rule with optional BreakpointType.
/// </summary>
internal record ObjectPositionRule(string Position, BreakpointType? breakpoint = null);
