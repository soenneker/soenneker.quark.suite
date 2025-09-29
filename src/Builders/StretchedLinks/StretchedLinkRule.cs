using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.StretchedLinks;

/// <summary>
/// Represents a single stretched link rule with optional BreakpointType.
/// </summary>
internal record StretchedLinkRule(BreakpointType? breakpoint = null);

