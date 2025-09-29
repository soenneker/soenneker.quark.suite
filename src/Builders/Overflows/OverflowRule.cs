using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Overflows;

/// <summary>
/// Represents a single overflow rule with optional BreakpointType.
/// </summary>
internal record OverflowRule(string Overflow, BreakpointType? breakpoint = null);
