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

    private const string _classRatio1x1 = "ratio ratio-1x1";
    private const string _classRatio4x3 = "ratio ratio-4x3";
    private const string _classRatio16x9 = "ratio ratio-16x9";
    private const string _classRatio21x9 = "ratio ratio-21x9";

    internal RatioBuilder(string ratio, BreakpointType? breakpoint = null)
    {
        _rules.Add(new RatioRule(ratio, breakpoint));
    }

    internal RatioBuilder(List<RatioRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public RatioBuilder R1x1 => ChainWithRatio("1x1");
    public RatioBuilder R4x3 => ChainWithRatio("4x3");
    public RatioBuilder R16x9 => ChainWithRatio("16x9");
    public RatioBuilder R21x9 => ChainWithRatio("21x9");

    public RatioBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public RatioBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public RatioBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public RatioBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public RatioBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public RatioBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new RatioRule(last.Ratio, breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetRatioClass(rule.Ratio);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

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
            "1x1" => _classRatio1x1,
            "4x3" => _classRatio4x3,
            "16x9" => _classRatio16x9,
            "21x9" => _classRatio21x9,
            _ => string.Empty
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}

