using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a text background rule with a color value and optional breakpoint
/// </summary>
public readonly record struct TextBackground(string Value, BreakpointType? Breakpoint = null);

