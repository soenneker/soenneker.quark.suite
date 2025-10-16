using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single gap rule with optional BreakpointType and direction.
/// </summary>
internal record GapRule(string Size, BreakpointType? breakpoint = null, string Direction = "");
