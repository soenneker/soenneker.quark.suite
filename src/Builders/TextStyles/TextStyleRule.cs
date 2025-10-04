using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct TextStyleRule(string Value, BreakpointType? Breakpoint);
