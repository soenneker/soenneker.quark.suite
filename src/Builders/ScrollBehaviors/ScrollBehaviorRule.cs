using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ScrollBehaviors;

/// <summary>
/// Represents a single scroll behavior rule with optional BreakpointType.
/// </summary>
internal record ScrollBehaviorRule(string Behavior, BreakpointType? breakpoint = null);

