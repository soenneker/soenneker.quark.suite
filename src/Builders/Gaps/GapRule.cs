using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single gap rule with optional BreakpointType.
/// </summary>
internal record GapRule(string Size, BreakpointType? breakpoint = null);
