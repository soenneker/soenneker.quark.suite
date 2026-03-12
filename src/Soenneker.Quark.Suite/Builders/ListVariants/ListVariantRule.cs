using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single list variant rule with optional BreakpointType.
/// </summary>
internal record ListVariantRule(ListVariantType Type, BreakpointType? Breakpoint = null);


