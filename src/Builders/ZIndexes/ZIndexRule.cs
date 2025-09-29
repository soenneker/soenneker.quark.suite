using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ZIndexes;

internal readonly record struct ZIndexRule(int Value, BreakpointType? breakpoint);

