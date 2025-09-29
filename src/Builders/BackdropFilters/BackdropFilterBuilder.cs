using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Builders.Abstract;
using Soenneker.Quark.Builders.Utils;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Builders.BackdropFilters;

/// <summary>
/// Simplified backdrop filter builder with fluent API for chaining backdrop filter rules.
/// </summary>
public sealed class BackdropFilterBuilder : ICssBuilder
{
    private readonly List<BackdropFilterRule> _rules = new(4);

    private const string _classBackdropFilterNone = "backdrop-filter-none";
    private const string _classBackdropFilterBlur = "backdrop-blur";
    private const string _classBackdropFilterBrightness = "backdrop-brightness";
    private const string _classBackdropFilterContrast = "backdrop-contrast";
    private const string _classBackdropFilterGrayscale = "backdrop-grayscale";
    private const string _classBackdropFilterHueRotate = "backdrop-hue-rotate";
    private const string _classBackdropFilterInvert = "backdrop-invert";
    private const string _classBackdropFilterOpacity = "backdrop-opacity";
    private const string _classBackdropFilterSaturate = "backdrop-saturate";
    private const string _classBackdropFilterSepia = "backdrop-sepia";

    internal BackdropFilterBuilder(string filter, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackdropFilterRule(filter, breakpoint));
    }

    internal BackdropFilterBuilder(List<BackdropFilterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BackdropFilterBuilder None => ChainWithFilter("none");
    public BackdropFilterBuilder Blur => ChainWithFilter("blur");
    public BackdropFilterBuilder Brightness => ChainWithFilter("brightness");
    public BackdropFilterBuilder Contrast => ChainWithFilter("contrast");
    public BackdropFilterBuilder Grayscale => ChainWithFilter("grayscale");
    public BackdropFilterBuilder HueRotate => ChainWithFilter("hue-rotate");
    public BackdropFilterBuilder Invert => ChainWithFilter("invert");
    public BackdropFilterBuilder Opacity => ChainWithFilter("opacity");
    public BackdropFilterBuilder Saturate => ChainWithFilter("saturate");
    public BackdropFilterBuilder Sepia => ChainWithFilter("sepia");

    public BackdropFilterBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public BackdropFilterBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public BackdropFilterBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public BackdropFilterBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public BackdropFilterBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public BackdropFilterBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackdropFilterBuilder ChainWithFilter(string filter)
    {
        _rules.Add(new BackdropFilterRule(filter, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackdropFilterBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BackdropFilterRule("none", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        BackdropFilterRule last = _rules[lastIdx];
        _rules[lastIdx] = new BackdropFilterRule(last.Filter, breakpoint);
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
            BackdropFilterRule rule = _rules[i];
            string cls = GetBackdropFilterClass(rule.Filter);
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
            BackdropFilterRule rule = _rules[i];
            string? filterValue = GetBackdropFilterValue(rule.Filter);

            if (filterValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("backdrop-filter: ");
            sb.Append(filterValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBackdropFilterClass(string filter)
    {
        return filter switch
        {
            "none" => _classBackdropFilterNone,
            "blur" => _classBackdropFilterBlur,
            "brightness" => _classBackdropFilterBrightness,
            "contrast" => _classBackdropFilterContrast,
            "grayscale" => _classBackdropFilterGrayscale,
            "hue-rotate" => _classBackdropFilterHueRotate,
            "invert" => _classBackdropFilterInvert,
            "opacity" => _classBackdropFilterOpacity,
            "saturate" => _classBackdropFilterSaturate,
            "sepia" => _classBackdropFilterSepia,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetBackdropFilterValue(string filter)
    {
        return filter switch
        {
            "none" => "none",
            "blur" => "blur(8px)",
            "brightness" => "brightness(1.1)",
            "contrast" => "contrast(1.1)",
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

