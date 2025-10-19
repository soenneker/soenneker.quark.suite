using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Represents a single display size rule with optional BreakpointType.
/// </summary>
internal record DisplaySizeRule(string Size, BreakpointType? breakpoint = null);


