using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

internal readonly record struct TextBreakRule(bool Enabled, BreakpointType? breakpoint);

