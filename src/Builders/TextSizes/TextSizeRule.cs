using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.TextSizes;

/// <summary>
/// Represents a single text size rule with optional BreakpointType.
/// </summary>
internal record TextSizeRule(string Size, BreakpointType? breakpoint = null);

