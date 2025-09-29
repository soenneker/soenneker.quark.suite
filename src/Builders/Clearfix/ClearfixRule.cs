using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Clearfix;

/// <summary>
/// Represents a single clearfix rule with optional BreakpointType.
/// </summary>
internal record ClearfixRule(BreakpointType? breakpoint = null);

