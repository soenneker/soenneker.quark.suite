using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified filter builder with fluent API for chaining filter rules.
/// </summary>
public sealed class FilterBuilder : ICssBuilder
{
    private readonly List<FilterRule> _rules = new(4);

    private const string _classFilterNone = "filter-none";
    private const string _classFilterBlur = "blur";
    private const string _classFilterBrightness = "brightness";
    private const string _classFilterContrast = "contrast";
    private const string _classFilterDropShadow = "drop-shadow";
    private const string _classFilterGrayscale = "grayscale";
    private const string _classFilterHueRotate = "hue-rotate";
    private const string _classFilterInvert = "invert";
    private const string _classFilterOpacity = "opacity";
    private const string _classFilterSaturate = "saturate";
    private const string _classFilterSepia = "sepia";

    internal FilterBuilder(string filter, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FilterRule(filter, breakpoint));
    }

    internal FilterBuilder(List<FilterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public FilterBuilder None => ChainWithFilter("none");
    public FilterBuilder Blur => ChainWithFilter("blur");
    public FilterBuilder Brightness => ChainWithFilter("brightness");
    public FilterBuilder Contrast => ChainWithFilter("contrast");
    public FilterBuilder DropShadow => ChainWithFilter("drop-shadow");
    public FilterBuilder Grayscale => ChainWithFilter("grayscale");
    public FilterBuilder HueRotate => ChainWithFilter("hue-rotate");
    public FilterBuilder Invert => ChainWithFilter("invert");
    public FilterBuilder Opacity => ChainWithFilter("opacity");
    public FilterBuilder Saturate => ChainWithFilter("saturate");
    public FilterBuilder Sepia => ChainWithFilter("sepia");

    public FilterBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public FilterBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public FilterBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public FilterBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public FilterBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public FilterBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FilterBuilder ChainWithFilter(string filter)
    {
        _rules.Add(new FilterRule(filter, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FilterBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FilterRule("none", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        FilterRule last = _rules[lastIdx];
        _rules[lastIdx] = new FilterRule(last.Filter, breakpoint);
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
            FilterRule rule = _rules[i];
            string cls = GetFilterClass(rule.Filter);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
            FilterRule rule = _rules[i];
            string? filterValue = GetFilterValue(rule.Filter);

            if (filterValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("filter: ");
            sb.Append(filterValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetFilterClass(string filter)
    {
        return filter switch
        {
            "none" => _classFilterNone,
            "blur" => _classFilterBlur,
            "brightness" => _classFilterBrightness,
            "contrast" => _classFilterContrast,
            "drop-shadow" => _classFilterDropShadow,
            "grayscale" => _classFilterGrayscale,
            "hue-rotate" => _classFilterHueRotate,
            "invert" => _classFilterInvert,
            "opacity" => _classFilterOpacity,
            "saturate" => _classFilterSaturate,
            "sepia" => _classFilterSepia,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetFilterValue(string filter)
    {
        return filter switch
        {
            "none" => "none",
            "blur" => "blur(8px)",
            "brightness" => "brightness(1.1)",
            "contrast" => "contrast(1.1)",
            "drop-shadow" => "drop-shadow(0 4px 6px -1px rgb(0 0 0 / 0.1))",
            "grayscale" => "grayscale(1)",
            "hue-rotate" => "hue-rotate(90deg)",
            "invert" => "invert(1)",
            "opacity" => "opacity(0.5)",
            "saturate" => "saturate(1.1)",
            "sepia" => "sepia(1)",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }

    public override string ToString()
    {
        return ToClass();
    }
}

