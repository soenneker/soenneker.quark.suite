using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Width builder with fluent API for chaining width rules.
/// Tailwind-first and shadcn-friendly (w-*, including fractions and common tokens).
/// </summary>
public sealed class WidthBuilder : ICssBuilder
{
    private readonly List<WidthRule> _rules = new(4);

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

    // Common Tailwind width tokens
    public WidthBuilder Is0 => ChainWithSize("0");
    public WidthBuilder IsPx => ChainWithSize("px");
    public WidthBuilder IsFull => ChainWithSize("full");
    public WidthBuilder IsScreen => ChainWithSize("screen");
    public WidthBuilder IsFit => ChainWithSize("fit");
    public WidthBuilder IsMin => ChainWithSize("min");
    public WidthBuilder IsMax => ChainWithSize("max");

    // Fractions
    public WidthBuilder Is1of2 => ChainWithSize("1/2");
    public WidthBuilder Is1of3 => ChainWithSize("1/3");
    public WidthBuilder Is2of3 => ChainWithSize("2/3");
    public WidthBuilder Is1of4 => ChainWithSize("1/4");
    public WidthBuilder Is2of4 => ChainWithSize("2/4");
    public WidthBuilder Is3of4 => ChainWithSize("3/4");
    public WidthBuilder Is1of5 => ChainWithSize("1/5");
    public WidthBuilder Is2of5 => ChainWithSize("2/5");
    public WidthBuilder Is3of5 => ChainWithSize("3/5");
    public WidthBuilder Is4of5 => ChainWithSize("4/5");
    public WidthBuilder Is1of6 => ChainWithSize("1/6");
    public WidthBuilder Is5of6 => ChainWithSize("5/6");
    public WidthBuilder Is1of12 => ChainWithSize("1/12");
    public WidthBuilder Is2of12 => ChainWithSize("2/12");
    public WidthBuilder Is3of12 => ChainWithSize("3/12");
    public WidthBuilder Is4of12 => ChainWithSize("4/12");
    public WidthBuilder Is5of12 => ChainWithSize("5/12");
    public WidthBuilder Is6of12 => ChainWithSize("6/12");
    public WidthBuilder Is7of12 => ChainWithSize("7/12");
    public WidthBuilder Is8of12 => ChainWithSize("8/12");
    public WidthBuilder Is9of12 => ChainWithSize("9/12");
    public WidthBuilder Is10of12 => ChainWithSize("10/12");
    public WidthBuilder Is11of12 => ChainWithSize("11/12");

    public WidthBuilder Auto => ChainWithSize("auto");

    public WidthBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    public WidthBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    public WidthBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    public WidthBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    public WidthBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    public WidthBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    /// <summary>
    /// Applies an arbitrary Tailwind width token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public WidthBuilder Token(string token) => ChainWithSize(token);

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
            _rules.Add(new WidthRule("full", breakpoint));
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

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetWidthClass(string size)
    {
        return size switch
        {
            "25" => "w-1/4",
            "50" => "w-1/2",
            "75" => "w-3/4",
            "100" => "w-full",
            "0" => "w-0",
            "px" => "w-px",
            "full" => "w-full",
            "screen" => "w-screen",
            "fit" => "w-fit",
            "min" => "w-min",
            "max" => "w-max",
            "1/2" => "w-1/2",
            "1/3" => "w-1/3",
            "2/3" => "w-2/3",
            "1/4" => "w-1/4",
            "2/4" => "w-2/4",
            "3/4" => "w-3/4",
            "1/5" => "w-1/5",
            "2/5" => "w-2/5",
            "3/5" => "w-3/5",
            "4/5" => "w-4/5",
            "1/6" => "w-1/6",
            "5/6" => "w-5/6",
            "1/12" => "w-1/12",
            "2/12" => "w-2/12",
            "3/12" => "w-3/12",
            "4/12" => "w-4/12",
            "5/12" => "w-5/12",
            "6/12" => "w-6/12",
            "7/12" => "w-7/12",
            "8/12" => "w-8/12",
            "9/12" => "w-9/12",
            "10/12" => "w-10/12",
            "11/12" => "w-11/12",
            "14" => "w-14",
            "16" => "w-16",
            "20" => "w-20",
            "24" => "w-24",
            "28" => "w-28",
            "32" => "w-32",
            "36" => "w-36",
            "40" => "w-40",
            "44" => "w-44",
            "48" => "w-48",
            "52" => "w-52",
            "56" => "w-56",
            "60" => "w-60",
            "64" => "w-64",
            "72" => "w-72",
            "80" => "w-80",
            "96" => "w-96",
            "auto" => "w-auto",
            _ when size.StartsWith("w-") => size,
            _ => string.Empty
        };
    }
}
