using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.PositionOffsets;

internal readonly record struct PositionOffsetRule(string Property, string Value, BreakpointType? breakpoint);

