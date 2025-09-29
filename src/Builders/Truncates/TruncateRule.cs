using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Truncates;

/// <summary>
/// Represents a single truncate rule with optional BreakpointType.
/// </summary>
internal record TruncateRule(BreakpointType? breakpoint = null);

