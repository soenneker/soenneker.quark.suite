using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct AlignRule(string Utility, string Value, BreakpointType? Breakpoint);
