namespace Soenneker.Quark;

/// <summary>
/// Represents a single start (inset-inline-start) rule with optional BreakpointType.
/// </summary>
internal record StartRule(string Value, BreakpointType? Breakpoint = null);
