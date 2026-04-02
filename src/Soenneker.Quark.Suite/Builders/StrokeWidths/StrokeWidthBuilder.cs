using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Stroke width builder for SVG/CSS. Tailwind: stroke-0, stroke-1, stroke-2.
/// </summary>
[TailwindPrefix("stroke-", Responsive = true)]
public sealed class StrokeWidthBuilder : ICssBuilder
{
    private readonly List<StrokeWidthRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal StrokeWidthBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeWidthRule(value, breakpoint));
    }

    internal StrokeWidthBuilder(List<StrokeWidthRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public StrokeWidthBuilder Is0 => Chain("0");
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public StrokeWidthBuilder Is1 => Chain("1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public StrokeWidthBuilder Is2 => Chain("2");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint.
    /// </summary>
    public StrokeWidthBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public StrokeWidthBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public StrokeWidthBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public StrokeWidthBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public StrokeWidthBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public StrokeWidthBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeWidthBuilder Chain(string value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new StrokeWidthRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeWidthBuilder ChainBp(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch { "0" => "stroke-0", "1" => "stroke-1", "2" => "stroke-2", _ => string.Empty };
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
        }
        return sb.ToString();
    }

    public string ToStyle()
    {
        return string.Empty;
    }

    public override string ToString() => ToClass();
}
