using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Filters;

/// <summary>
/// Represents a single filter rule with optional BreakpointType.
/// </summary>
internal record FilterRule(string Filter, BreakpointType? breakpoint = null);

