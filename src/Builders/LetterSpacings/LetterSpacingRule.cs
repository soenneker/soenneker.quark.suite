using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct LetterSpacingRule(string Utility, string Value, BreakpointType? Breakpoint);
