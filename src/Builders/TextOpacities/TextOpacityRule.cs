using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single text opacity rule with optional BreakpointType.
/// </summary>
internal record TextOpacityRule(int Value, BreakpointType? breakpoint = null);




