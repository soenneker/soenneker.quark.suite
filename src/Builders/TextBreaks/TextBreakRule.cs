using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.TextBreaks;

internal readonly record struct TextBreakRule(bool Enabled, BreakpointType? breakpoint);

