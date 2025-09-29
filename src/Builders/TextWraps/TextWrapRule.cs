using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.TextWraps;

internal readonly record struct TextWrapRule(string Value, BreakpointType? breakpoint);

