using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single background opacity rule with optional BreakpointType.
/// </summary>
internal record BackgroundOpacityRule(int Value, BreakpointType? breakpoint = null);



