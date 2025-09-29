using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Widths;

/// <summary>
/// Represents a single width rule with optional BreakpointType.
/// </summary>
internal record WidthRule(string Size, BreakpointType? breakpoint = null);

