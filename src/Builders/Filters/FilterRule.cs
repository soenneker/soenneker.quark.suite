using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single filter rule with optional BreakpointType.
/// </summary>
internal record FilterRule(string Filter, BreakpointType? Breakpoint = null);

