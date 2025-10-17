using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

public readonly record struct TextBackgroundRule(string Value, BreakpointType? breakpoint = null);

