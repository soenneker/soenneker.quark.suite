using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single backdrop filter rule with optional BreakpointType.
/// </summary>
internal record BackdropFilterRule(string Filter, BreakpointType? breakpoint = null);

