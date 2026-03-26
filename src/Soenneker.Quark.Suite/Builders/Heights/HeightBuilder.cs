using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Height builder with fluent API for chaining height rules.
/// Tailwind-first and shadcn-friendly (h-* and common tokens).
/// </summary>
[TailwindPrefix("h-", Responsive = true)]
public sealed class HeightBuilder : ICssBuilder
{
    private readonly List<HeightRule> _rules = new(4);

    internal HeightBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new HeightRule(size, breakpoint));
    }

    internal HeightBuilder(List<HeightRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public HeightBuilder Is25 => ChainWithSize("25");
    public HeightBuilder Is50 => ChainWithSize("50");
    public HeightBuilder Is75 => ChainWithSize("75");
    public HeightBuilder Is100 => ChainWithSize("100");

    public HeightBuilder Is0 => ChainWithSize("0");
    public HeightBuilder Is8 => ChainWithSize("8");
    public HeightBuilder IsPx => ChainWithSize("px");
    public HeightBuilder IsFull => ChainWithSize("full");
    public HeightBuilder IsScreen => ChainWithSize("screen");
    public HeightBuilder IsFit => ChainWithSize("fit");
    public HeightBuilder IsMin => ChainWithSize("min");
    public HeightBuilder IsMax => ChainWithSize("max");

    public HeightBuilder Is1of2 => ChainWithSize("1/2");
    public HeightBuilder Is1of3 => ChainWithSize("1/3");
    public HeightBuilder Is2of3 => ChainWithSize("2/3");
    public HeightBuilder Is1of4 => ChainWithSize("1/4");
    public HeightBuilder Is2of4 => ChainWithSize("2/4");
    public HeightBuilder Is3of4 => ChainWithSize("3/4");

    public HeightBuilder Auto => ChainWithSize("auto");

    public HeightBuilder OnBase => ChainWithBreakpoint(BreakpointType.Base);
    public HeightBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    public HeightBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    public HeightBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    public HeightBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    public HeightBuilder OnXxl => ChainWithBreakpoint(BreakpointType.Xxl);

    /// <summary>
    /// Applies an arbitrary Tailwind height token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public HeightBuilder Token(string token) => ChainWithSize(token);

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
            _rules.Add(new HeightRule("full", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
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
            var rule = _rules[i];
            var cls = GetHeightClass(rule.Size);
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
    private static string GetHeightClass(string size)
    {
        return size switch
        {
            "25" => "h-1/4",
            "50" => "h-1/2",
            "75" => "h-3/4",
            "100" => "h-full",
            "0" => "h-0",
            "8" => "h-8",
            "px" => "h-px",
            "full" => "h-full",
            "screen" => "h-screen",
            "fit" => "h-fit",
            "min" => "h-min",
            "max" => "h-max",
            "1/2" => "h-1/2",
            "1/3" => "h-1/3",
            "2/3" => "h-2/3",
            "1/4" => "h-1/4",
            "2/4" => "h-2/4",
            "3/4" => "h-3/4",
            "14" => "h-14",
            "16" => "h-16",
            "20" => "h-20",
            "24" => "h-24",
            "28" => "h-28",
            "32" => "h-32",
            "36" => "h-36",
            "40" => "h-40",
            "44" => "h-44",
            "48" => "h-48",
            "52" => "h-52",
            "56" => "h-56",
            "60" => "h-60",
            "64" => "h-64",
            "72" => "h-72",
            "80" => "h-80",
            "96" => "h-96",
            "auto" => "h-auto",
            _ when size.StartsWith("h-") => size,
            _ => string.Empty
        };
    }
}