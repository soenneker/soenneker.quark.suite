using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Animations;

/// <summary>
/// Represents a single animation rule with optional BreakpointType.
/// </summary>
internal record AnimationRule(string Animation, BreakpointType? breakpoint = null);
