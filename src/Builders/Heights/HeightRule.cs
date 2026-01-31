using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single height rule with optional BreakpointType.
/// </summary>
internal record HeightRule(string Size, BreakpointType? Breakpoint = null);
