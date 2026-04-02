namespace Soenneker.Quark;

/// <summary>
/// Represents a single mix-blend-mode rule with optional BreakpointType.
/// </summary>
internal record MixBlendModeRule(string Value, BreakpointType? Breakpoint = null);
