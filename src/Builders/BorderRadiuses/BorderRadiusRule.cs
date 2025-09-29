using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.BorderRadiuses;

/// <summary>
/// Represents a single border radius rule with optional BreakpointType.
/// </summary>
internal record BorderRadiusRule(string Size, ElementSideType Side, BreakpointType? breakpoint = null, string CornerToken = "");
