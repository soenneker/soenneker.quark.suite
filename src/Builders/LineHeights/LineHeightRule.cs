using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.LineHeights;

internal readonly record struct LineHeightRule(string Value, BreakpointType? breakpoint);
