using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Heights;

/// <summary>
/// Represents a single height rule with optional BreakpointType.
/// </summary>
internal record HeightRule(string Size, BreakpointType? breakpoint = null);
