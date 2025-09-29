using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Interactions;

/// <summary>
/// Represents a single interaction rule with optional BreakpointType.
/// </summary>
internal record InteractionRule(string UserSelect, string PointerEvents, BreakpointType? breakpoint = null);
