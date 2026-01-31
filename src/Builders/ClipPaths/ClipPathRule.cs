using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single clip path rule with optional BreakpointType.
/// </summary>
internal record ClipPathRule(string Path, BreakpointType? Breakpoint = null);
