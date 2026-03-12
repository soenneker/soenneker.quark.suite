using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single link opacity rule with optional BreakpointType.
/// </summary>
internal record LinkOpacityRule(int Value, BreakpointType? Breakpoint = null);







