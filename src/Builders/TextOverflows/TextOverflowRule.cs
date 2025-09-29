using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.TextOverflows;

/// <summary>
/// Represents a single text overflow rule with optional BreakpointType.
/// </summary>
internal record TextOverflowRule(string Value, BreakpointType? breakpoint = null);

