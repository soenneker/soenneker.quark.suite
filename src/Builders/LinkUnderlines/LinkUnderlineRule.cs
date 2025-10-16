using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single link underline rule with optional BreakpointType, opacity, and color.
/// </summary>
internal record LinkUnderlineRule(string Value, string Type, BreakpointType? breakpoint = null);
// Type can be: "base", "opacity", "color"

