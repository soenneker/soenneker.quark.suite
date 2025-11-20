using Soenneker.Quark.Enums;
using System;
using System.Collections.Generic;
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

    public WidthBuilder Is25 => ChainWithSize("25");
    public WidthBuilder Is50 => ChainWithSize("50");
    public WidthBuilder Is75 => ChainWithSize("75");
    public WidthBuilder Is100 => ChainWithSize("100");
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
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
            _ => size
        };
    }


}

