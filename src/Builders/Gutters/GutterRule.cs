using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single gutter rule with value, type, and breakpoint.
/// </summary>
internal readonly record struct GutterRule(int Value, GutterType Type, GutterBreakpoint? Breakpoint);