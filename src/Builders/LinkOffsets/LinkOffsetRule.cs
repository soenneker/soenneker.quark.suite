using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single link offset rule with optional BreakpointType.
/// </summary>
internal record LinkOffsetRule(int Value, BreakpointType? Breakpoint = null);







