namespace Soenneker.Quark;

/// <summary>
/// One aspect-ratio utility rule for Tailwind <c>aspect-*</c> classes, optionally scoped to a responsive breakpoint.
/// </summary>
/// <param name="Ratio">Tailwind ratio token (for example <c>video</c>, <c>square</c>, or arbitrary <c>[4/3]</c>).</param>
/// <param name="Breakpoint">When set, prefixes the utility with that breakpoint (for example <c>md:aspect-video</c>).</param>
public readonly record struct RatioRule(string Ratio, BreakpointType? Breakpoint = null);

