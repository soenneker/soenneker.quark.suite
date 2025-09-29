using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Margins;

/// <summary>
/// Represents a single margin rule with optional BreakpointType.
/// </summary>
internal record MarginRule(string Size, ElementSideType Side, BreakpointType? breakpoint = null);
