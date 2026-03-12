using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single flex rule with optional BreakpointType.
/// </summary>
internal record FlexRule(string Property, string Value, BreakpointType? Breakpoint = null);

