using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.AspectRatios;

/// <summary>
/// Represents a single aspect ratio rule with optional BreakpointType.
/// </summary>
internal record AspectRatioRule(string Ratio, BreakpointType? breakpoint = null);
