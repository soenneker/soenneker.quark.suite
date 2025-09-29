using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ObjectFits;

/// <summary>
/// Represents a single object-fit rule with optional BreakpointType.
/// </summary>
internal record ObjectFitRule(string Fit, BreakpointType? breakpoint = null);
