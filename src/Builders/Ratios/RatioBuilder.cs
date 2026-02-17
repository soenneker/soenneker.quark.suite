using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap ratio container builder for maintaining aspect ratios.
/// Produces Bootstrap ratio utility classes like ratio-16x9.
/// Note: This is different from AspectRatioBuilder which uses the aspect-ratio CSS property.
/// </summary>
public sealed class RatioBuilder : ICssBuilder
{
    private readonly List<RatioRule> _rules = new(4);

    private const string _classRatio1X1 = "ratio ratio-1x1";
    private const string _classRatio4X3 = "ratio ratio-4x3";
    private const string _classRatio16X9 = "ratio ratio-16x9";
    private const string _classRatio21X9 = "ratio ratio-21x9";

    internal RatioBuilder(string ratio, BreakpointType? breakpoint = null)
    {
        _rules.Add(new RatioRule(ratio, breakpoint));
    }

    internal RatioBuilder(List<RatioRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the ratio to 1:1 (square).
    /// </summary>
    public RatioBuilder R1X1 => ChainWithRatio("1x1");
    /// <summary>
    /// Sets the ratio to 4:3.
    /// </summary>
    public RatioBuilder R4X3 => ChainWithRatio("4x3");
    /// <summary>
    /// Sets the ratio to 16:9.
    /// </summary>
    public RatioBuilder R16X9 => ChainWithRatio("16x9");
    /// <summary>
    /// Sets the ratio to 21:9.
    /// </summary>
    public RatioBuilder R21X9 => ChainWithRatio("21x9");

    /// <summary>
    /// Applies the ratio on phone breakpoint.
    /// </summary>
    public RatioBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the ratio on tablet breakpoint.
    /// </summary>
    public RatioBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the ratio on laptop breakpoint.
    /// </summary>
    public RatioBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the ratio on desktop breakpoint.
    /// </summary>
    public RatioBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the ratio on widescreen breakpoint.
    /// </summary>
    public RatioBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the ratio on ultrawide breakpoint.
    /// </summary>
    public RatioBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RatioBuilder ChainWithRatio(string ratio)
    {
        _rules.Add(new RatioRule(ratio, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RatioBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new RatioRule("16x9", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        RatioRule last = _rules[lastIdx];
        _rules[lastIdx] = new RatioRule(last.Ratio, breakpoint);
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
            RatioRule rule = _rules[i];
            string cls = GetRatioClass(rule.Ratio);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Bootstrap ratio utilities should use classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as ratios are handled via classes.</returns>
    public string ToStyle()
    {
        // Bootstrap ratio utilities should use classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetRatioClass(string ratio)
    {
        return ratio switch
        {
            "1x1" => _classRatio1X1,
            "4x3" => _classRatio4X3,
            "16x9" => _classRatio16X9,
            "21x9" => _classRatio21X9,
            _ => string.Empty
        };
    }

    /// <summary>
    /// Returns the CSS class string representation of this ratio builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

