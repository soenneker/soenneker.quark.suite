using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single object-fit rule with optional BreakpointType.
/// </summary>
internal record ObjectFitRule(string Fit, BreakpointType? breakpoint = null);
