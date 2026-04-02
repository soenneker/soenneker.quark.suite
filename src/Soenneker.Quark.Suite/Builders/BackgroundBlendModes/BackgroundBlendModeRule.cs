namespace Soenneker.Quark;

/// <summary>
/// Represents a single background-blend-mode rule with optional BreakpointType.
/// </summary>
internal record BackgroundBlendModeRule(string Value, BreakpointType? Breakpoint = null);
