using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single transform rule with optional BreakpointType.
/// </summary>
internal record TransformRule(string Transform, BreakpointType? Breakpoint = null);

