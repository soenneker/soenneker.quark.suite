using Soenneker.Quark.Enums;

using System;
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

    private const string _classAspect1x1 = "aspect-1x1";
    private const string _classAspect4x3 = "aspect-4x3";
    private const string _classAspect16x9 = "aspect-16x9";
    private const string _classAspect21x9 = "aspect-21x9";

    internal AspectRatioBuilder(string ratio, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AspectRatioRule(ratio, breakpoint));
    }

    internal AspectRatioBuilder(List<AspectRatioRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public AspectRatioBuilder R1x1 => ChainWithRatio("1x1");
    public AspectRatioBuilder R4x3 => ChainWithRatio("4x3");
    public AspectRatioBuilder R16x9 => ChainWithRatio("16x9");
    public AspectRatioBuilder R21x9 => ChainWithRatio("21x9");

    public AspectRatioBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public AspectRatioBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public AspectRatioBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public AspectRatioBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public AspectRatioBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public AspectRatioBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new AspectRatioRule(last.Ratio, breakpoint);
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
            var cls = GetAspectRatioClass(rule.Ratio);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var ratioValue = GetAspectRatioValue(rule.Ratio);

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
            "1x1" => _classAspect1x1,
            "4x3" => _classAspect4x3,
            "16x9" => _classAspect16x9,
            "21x9" => _classAspect21x9,
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }

    public override string ToString()
    {
        return ToClass();
    }
}
