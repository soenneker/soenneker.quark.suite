using Soenneker.Quark.Enums;


namespace Soenneker.Quark.Builders.FontWeights;

internal readonly record struct FontWeightRule(string Value, BreakpointType? breakpoint);
