using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.BoxShadows;

internal readonly record struct BoxShadowRule(string Value, BreakpointType? breakpoint);

