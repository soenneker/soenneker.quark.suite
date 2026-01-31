using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single stretched link rule with optional BreakpointType.
/// </summary>
internal record StretchedLinkRule(BreakpointType? Breakpoint = null);

