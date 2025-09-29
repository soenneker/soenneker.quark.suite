using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Flexes;

/// <summary>
/// Represents a single flex rule with optional BreakpointType.
/// </summary>
internal record FlexRule(string Property, string Value, BreakpointType? breakpoint = null);

