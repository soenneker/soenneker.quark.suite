using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a single Tailwind/shadcn radius rule.
/// </summary>
/// <param name="SizeToken">
/// Radius size token (null = default 'rounded').
/// Examples: sm, md, lg, xl, 2xl, 3xl, full, none.
/// </param>
/// <param name="PositionToken">
/// Position token (null = all).
/// Examples: t, b, l, r, tl, tr, bl, br.
/// </param>
/// <param name="Breakpoint">
/// Optional responsive breakpoint.
/// </param>
internal readonly record struct RadiusRule(string? SizeToken, string? PositionToken, BreakpointType? Breakpoint = null);