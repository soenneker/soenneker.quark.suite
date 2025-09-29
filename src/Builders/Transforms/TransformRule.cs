using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Transforms;

/// <summary>
/// Represents a single transform rule with optional BreakpointType.
/// </summary>
internal record TransformRule(string Transform, BreakpointType? breakpoint = null);

