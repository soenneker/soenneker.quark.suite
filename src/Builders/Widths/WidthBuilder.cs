using System;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified width builder with fluent API for chaining width rules.
/// </summary>
public sealed class WidthBuilder : ICssBuilder
{
    private readonly List<WidthRule> _rules = new(4);

    private const string _classW25 = "w-25";
    private const string _classW50 = "w-50";
    private const string _classW75 = "w-75";
    private const string _classW100 = "w-100";
    private const string _classWAuto = "w-auto";
    private const string _widthPrefix = "width: ";

    internal WidthBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new WidthRule(size, breakpoint));
    }

    internal WidthBuilder(List<WidthRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public WidthBuilder P25 => ChainWithSize("25");
    public WidthBuilder P50 => ChainWithSize("50");
    public WidthBuilder P75 => ChainWithSize("75");
    public WidthBuilder P100 => ChainWithSize("100");
    public WidthBuilder Auto => ChainWithSize("auto");

    public WidthBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public WidthBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public WidthBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public WidthBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public WidthBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public WidthBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder ChainWithSize(string size)
    {
        _rules.Add(new WidthRule(size, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new WidthRule("100", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new WidthRule(last.Size, breakpoint);
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
            var cls = GetWidthClass(rule.Size);
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
            var val = GetWidthValue(rule.Size);
            if (val is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_widthPrefix);
            sb.Append(val);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetWidthClass(string size)
    {
        return size switch
        {
            "25" => _classW25,
            "50" => _classW50,
            "75" => _classW75,
            "100" => _classW100,
            "auto" => _classWAuto,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetWidthValue(string size)
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
}

