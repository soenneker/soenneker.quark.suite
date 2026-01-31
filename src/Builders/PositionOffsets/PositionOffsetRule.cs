using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

internal readonly record struct PositionOffsetRule(string Property, string Value, BreakpointType? Breakpoint);

