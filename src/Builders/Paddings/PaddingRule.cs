using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Paddings;

/// <summary>
/// Represents a single padding rule with optional BreakpointType.
/// </summary>
internal record PaddingRule(string Size, ElementSideType Side, BreakpointType? breakpoint = null);

