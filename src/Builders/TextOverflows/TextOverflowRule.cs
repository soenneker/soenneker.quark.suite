using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single text overflow rule with optional BreakpointType.
/// </summary>
internal record TextOverflowRule(string Value, BreakpointType? Breakpoint = null);

