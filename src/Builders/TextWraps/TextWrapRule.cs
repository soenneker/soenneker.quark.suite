using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

internal readonly record struct TextWrapRule(string Value, BreakpointType? breakpoint);

