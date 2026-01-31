using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single transition rule with optional BreakpointType.
/// </summary>
internal record TransitionRule(string Transition, BreakpointType? Breakpoint = null);

