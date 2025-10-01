using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single display rule with optional BreakpointType.
/// </summary>
internal record DisplayRule(string Display, BreakpointType? breakpoint = null);

