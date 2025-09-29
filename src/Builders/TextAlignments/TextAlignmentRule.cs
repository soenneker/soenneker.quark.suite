using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.TextAlignments;

internal readonly record struct TextAlignmentRule(string Value, BreakpointType? breakpoint);

