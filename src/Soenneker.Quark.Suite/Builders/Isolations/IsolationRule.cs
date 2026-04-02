namespace Soenneker.Quark;

/// <summary>
/// Represents a single isolation rule with optional BreakpointType.
/// </summary>
internal record IsolationRule(string Value, BreakpointType? Breakpoint = null);
