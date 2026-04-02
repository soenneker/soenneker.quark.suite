using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Width builder with fluent API for chaining width rules.
/// Tailwind-first and shadcn-friendly (w-*, including fractions and common tokens).
/// </summary>
[TailwindPrefix("w-", Responsive = true)]
public sealed class WidthBuilder : ICssBuilder
{
    private readonly List<WidthRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal WidthBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new WidthRule(size, breakpoint));
    }

    internal WidthBuilder(List<WidthRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// <c>w-25</c> — width from the spacing scale (<c>25</c> → <c>6.25rem</c> in Tailwind’s default theme).
    /// </summary>
    public WidthBuilder Is25 => ChainWithSize("25");
    /// <summary>
    /// <c>w-50</c> — spacing scale step <c>50</c> (<c>12.5rem</c> by default).
    /// </summary>
    public WidthBuilder Is50 => ChainWithSize("50");
    /// <summary>
    /// <c>w-75</c> — spacing scale step <c>75</c> (<c>18.75rem</c> by default).
    /// </summary>
    public WidthBuilder Is75 => ChainWithSize("75");
    /// <summary>
    /// <c>w-100</c> — spacing scale step <c>100</c> (<c>25rem</c> by default).
    /// </summary>
    public WidthBuilder Is100 => ChainWithSize("100");

    // Common Tailwind width tokens
    /// <summary>
    /// <c>w-0</c> — <c>width: 0px</c>.
    /// </summary>
    public WidthBuilder Is0 => ChainWithSize("0");
    /// <summary>
    /// <c>w-px</c> — fixed <c>1px</c> width.
    /// </summary>
    public WidthBuilder IsPx => ChainWithSize("px");
    /// <summary>
    /// <c>w-full</c> — <c>width: 100%</c> of the parent.
    /// </summary>
    public WidthBuilder IsFull => ChainWithSize("full");
    /// <summary>
    /// <c>w-screen</c> — <c>100vw</c> (viewport width).
    /// </summary>
    public WidthBuilder IsScreen => ChainWithSize("screen");
    /// <summary>
    /// <c>w-fit</c> — <c>width: fit-content</c>.
    /// </summary>
    public WidthBuilder IsFit => ChainWithSize("fit");
    /// <summary>
    /// <c>w-min</c> — <c>width: min-content</c>.
    /// </summary>
    public WidthBuilder IsMin => ChainWithSize("min");
    /// <summary>
    /// <c>w-max</c> — <c>width: max-content</c>.
    /// </summary>
    public WidthBuilder IsMax => ChainWithSize("max");

    // Fractions
    /// <summary>
    /// <c>w-1/2</c> — <c>width: 50%</c>.
    /// </summary>
    public WidthBuilder Is1of2 => ChainWithSize("1/2");
    /// <summary>
    /// <c>w-1/3</c> — <c>width: 33.333333%</c>.
    /// </summary>
    public WidthBuilder Is1of3 => ChainWithSize("1/3");
    /// <summary>
    /// <c>w-2/3</c> — <c>width: 66.666667%</c>.
    /// </summary>
    public WidthBuilder Is2of3 => ChainWithSize("2/3");
    /// <summary>
    /// <c>w-1/4</c> — <c>width: 25%</c>.
    /// </summary>
    public WidthBuilder Is1of4 => ChainWithSize("1/4");
    /// <summary>
    /// <c>w-2/4</c> — same as <c>w-1/2</c> (<c>50%</c>).
    /// </summary>
    public WidthBuilder Is2of4 => ChainWithSize("2/4");
    /// <summary>
    /// <c>w-3/4</c> — <c>width: 75%</c>.
    /// </summary>
    public WidthBuilder Is3of4 => ChainWithSize("3/4");
    /// <summary>
    /// <c>w-1/5</c> — <c>width: 20%</c>.
    /// </summary>
    public WidthBuilder Is1of5 => ChainWithSize("1/5");
    /// <summary>
    /// <c>w-2/5</c> — <c>width: 40%</c>.
    /// </summary>
    public WidthBuilder Is2of5 => ChainWithSize("2/5");
    /// <summary>
    /// <c>w-3/5</c> — <c>width: 60%</c>.
    /// </summary>
    public WidthBuilder Is3of5 => ChainWithSize("3/5");
    /// <summary>
    /// <c>w-4/5</c> — <c>width: 80%</c>.
    /// </summary>
    public WidthBuilder Is4of5 => ChainWithSize("4/5");
    /// <summary>
    /// <c>w-1/6</c> — <c>width: 16.666667%</c>.
    /// </summary>
    public WidthBuilder Is1of6 => ChainWithSize("1/6");
    /// <summary>
    /// <c>w-5/6</c> — <c>width: 83.333333%</c>.
    /// </summary>
    public WidthBuilder Is5of6 => ChainWithSize("5/6");
    /// <summary>
    /// <c>w-1/12</c> — one twelfth of the container (<c>8.333333%</c>); common for 12-column grids.
    /// </summary>
    public WidthBuilder Is1of12 => ChainWithSize("1/12");
    /// <summary>
    /// <c>w-2/12</c> — <c>16.666667%</c> (equivalent to <c>w-1/6</c>).
    /// </summary>
    public WidthBuilder Is2of12 => ChainWithSize("2/12");
    /// <summary>
    /// <c>w-3/12</c> — <c>25%</c> (equivalent to <c>w-1/4</c>).
    /// </summary>
    public WidthBuilder Is3of12 => ChainWithSize("3/12");
    /// <summary>
    /// <c>w-4/12</c> — <c>33.333333%</c> (equivalent to <c>w-1/3</c>).
    /// </summary>
    public WidthBuilder Is4of12 => ChainWithSize("4/12");
    /// <summary>
    /// <c>w-5/12</c> — <c>41.666667%</c>.
    /// </summary>
    public WidthBuilder Is5of12 => ChainWithSize("5/12");
    /// <summary>
    /// <c>w-6/12</c> — <c>50%</c> (equivalent to <c>w-1/2</c>).
    /// </summary>
    public WidthBuilder Is6of12 => ChainWithSize("6/12");
    /// <summary>
    /// <c>w-7/12</c> — <c>58.333333%</c>.
    /// </summary>
    public WidthBuilder Is7of12 => ChainWithSize("7/12");
    /// <summary>
    /// <c>w-8/12</c> — <c>66.666667%</c> (equivalent to <c>w-2/3</c>).
    /// </summary>
    public WidthBuilder Is8of12 => ChainWithSize("8/12");
    /// <summary>
    /// <c>w-9/12</c> — <c>75%</c> (equivalent to <c>w-3/4</c>).
    /// </summary>
    public WidthBuilder Is9of12 => ChainWithSize("9/12");
    /// <summary>
    /// <c>w-10/12</c> — <c>83.333333%</c> (equivalent to <c>w-5/6</c>).
    /// </summary>
    public WidthBuilder Is10of12 => ChainWithSize("10/12");
    /// <summary>
    /// <c>w-11/12</c> — <c>91.666667%</c>.
    /// </summary>
    public WidthBuilder Is11of12 => ChainWithSize("11/12");

    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public WidthBuilder Auto => ChainWithSize("auto");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public WidthBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public WidthBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public WidthBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public WidthBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public WidthBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public WidthBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    /// <summary>
    /// Applies an arbitrary Tailwind width token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public WidthBuilder Token(string token) => ChainWithSize(token);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder ChainWithSize(string size)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new WidthRule(size, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
            _ when size.Length > 0 => "w-" + size,
            _ => string.Empty
        };
    }
}