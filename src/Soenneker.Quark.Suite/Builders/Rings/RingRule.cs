using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct RingRule(string Token, BreakpointType? Breakpoint = null);
