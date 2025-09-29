using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Visibilities;

internal readonly record struct VisibilityRule(string Value, BreakpointType? breakpoint);

