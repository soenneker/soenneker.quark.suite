namespace Soenneker.Quark;

/// <summary>
/// Represents a single fill-rule (SVG) rule with optional BreakpointType.
/// </summary>
internal record FillRuleRule(string Value, BreakpointType? Breakpoint = null);
