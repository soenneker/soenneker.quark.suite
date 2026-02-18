using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct GridRule(string Utility, string Value, BreakpointType? Breakpoint);
