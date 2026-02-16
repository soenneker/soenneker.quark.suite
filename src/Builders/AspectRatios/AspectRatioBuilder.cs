using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified aspect ratio builder with fluent API for chaining aspect ratio rules.
/// </summary>
public sealed class AspectRatioBuilder : ICssBuilder
{
    private readonly List<AspectRatioRule> _rules = new(4);

    private const string _classAspect1X1 = "aspect-1x1";
    private const string _classAspect4X3 = "aspect-4x3";
    private const string _classAspect16X9 = "aspect-16x9";
    private const string _classAspect21X9 = "aspect-21x9";

    internal AspectRatioBuilder(string ratio, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AspectRatioRule(ratio, breakpoint));
    }

    internal AspectRatioBuilder(List<AspectRatioRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the aspect ratio to 1:1 (square).
    /// </summary>
    public AspectRatioBuilder R1X1 => ChainWithRatio("1x1");
    /// <summary>
    /// Sets the aspect ratio to 4:3.
    /// </summary>
    public AspectRatioBuilder R4X3 => ChainWithRatio("4x3");
    /// <summary>
    /// Sets the aspect ratio to 16:9.
    /// </summary>
    public AspectRatioBuilder R16X9 => ChainWithRatio("16x9");
    /// <summary>
    /// Sets the aspect ratio to 21:9.
    /// </summary>
    public AspectRatioBuilder R21X9 => ChainWithRatio("21x9");

    /// <summary>
    /// Applies the aspect ratio at base (mobile-first default).
    /// </summary>
    public AspectRatioBuilder OnBase => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the aspect ratio at sm breakpoint (≥640px).
    /// </summary>
    public AspectRatioBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the aspect ratio at md breakpoint (≥768px).
    /// </summary>
    public AspectRatioBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the aspect ratio at lg breakpoint (≥1024px).
    /// </summary>
    public AspectRatioBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the aspect ratio at xl breakpoint (≥1280px).
    /// </summary>
    public AspectRatioBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the aspect ratio at 2xl breakpoint (≥1536px).
    /// </summary>
    public AspectRatioBuilder On2xl => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AspectRatioBuilder ChainWithRatio(string ratio)
    {
        _rules.Add(new AspectRatioRule(ratio, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AspectRatioBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new AspectRatioRule("1x1", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        AspectRatioRule last = _rules[lastIdx];
        _rules[lastIdx] = new AspectRatioRule(last.Ratio, breakpoint);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            AspectRatioRule rule = _rules[i];
            string cls = GetAspectRatioClass(rule.Ratio);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            AspectRatioRule rule = _rules[i];
            string? ratioValue = GetAspectRatioValue(rule.Ratio);

            if (ratioValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("aspect-ratio: ");
            sb.Append(ratioValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetAspectRatioClass(string ratio)
    {
        return ratio switch
        {
            "1x1" => _classAspect1X1,
            "4x3" => _classAspect4X3,
            "16x9" => _classAspect16X9,
            "21x9" => _classAspect21X9,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetAspectRatioValue(string ratio)
    {
        return ratio switch
        {
            "1x1" => "1 / 1",
            "4x3" => "4 / 3",
            "16x9" => "16 / 9",
            "21x9" => "21 / 9",
            _ => ratio
        };
    }

    /// <summary>
    /// Returns the CSS class string representation of this aspect ratio builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}
