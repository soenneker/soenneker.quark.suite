using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Resizes;

/// <summary>
/// Represents a single resize rule with optional BreakpointType.
/// </summary>
internal record ResizeRule(string Resize, BreakpointType? breakpoint = null);

