using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct GradientRule(string Utility, string Value, BreakpointType? Breakpoint);
