using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single focus ring rule with optional BreakpointType.
/// </summary>
internal record FocusRingRule(string Color, BreakpointType? breakpoint = null);





