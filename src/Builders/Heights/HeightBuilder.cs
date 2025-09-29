using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified height builder with fluent API for chaining height rules.
/// </summary>
public sealed class HeightBuilder : ICssBuilder
{
    private readonly List<HeightRule> _rules = new(4);

    private const string _classH25 = "h-25";
    private const string _classH50 = "h-50";
    private const string _classH75 = "h-75";
    private const string _classH100 = "h-100";
    private const string _classHAuto = "h-auto";
    private const string _heightPrefix = "height: ";

    internal HeightBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new HeightRule(size, breakpoint));
    }

    internal HeightBuilder(List<HeightRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public HeightBuilder P25 => ChainWithSize("25");
    public HeightBuilder P50 => ChainWithSize("50");
    public HeightBuilder P75 => ChainWithSize("75");
    public HeightBuilder P100 => ChainWithSize("100");
    public HeightBuilder Auto => ChainWithSize("auto");

    public HeightBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public HeightBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public HeightBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public HeightBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public HeightBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public HeightBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private HeightBuilder ChainWithSize(string size)
    {
        _rules.Add(new HeightRule(size, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private HeightBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new HeightRule("100", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        HeightRule last = _rules[lastIdx];
        _rules[lastIdx] = new HeightRule(last.Size, breakpoint);
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
            HeightRule rule = _rules[i];
            string cls = GetHeightClass(rule.Size);
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
            HeightRule rule = _rules[i];
            string? val = GetHeightValue(rule.Size);
            if (val is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_heightPrefix);
            sb.Append(val);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetHeightClass(string size)
    {
        return size switch
        {
            "25" => _classH25,
            "50" => _classH50,
            "75" => _classH75,
            "100" => _classH100,
            "auto" => _classHAuto,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetHeightValue(string size)
    {
        return size switch
        {
            "25" => "25%",
            "50" => "50%",
            "75" => "75%",
            "100" => "100%",
            "auto" => "auto",
            _ => IsNumeric(size) ? $"{size}px" : null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsNumeric(string value)
    {
        return !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
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
}
