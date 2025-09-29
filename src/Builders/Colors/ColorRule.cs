using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Colors;

internal readonly record struct ColorRule(string Value, BreakpointType? breakpoint);
