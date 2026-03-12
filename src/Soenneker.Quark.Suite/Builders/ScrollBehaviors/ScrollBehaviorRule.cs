using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single scroll behavior rule with optional BreakpointType.
/// </summary>
internal record ScrollBehaviorRule(string Behavior, BreakpointType? Breakpoint = null);

