using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Tailwind overscroll-behavior builder supporting both axis-specific and global utilities.
/// </summary>
[TailwindPrefix("overscroll-", Responsive = true)]
public sealed class OverscrollBuilder : ICssBuilder
{
    private readonly List<OverscrollRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal OverscrollBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new OverscrollRule(value, breakpoint));
    }

    internal OverscrollBuilder(List<OverscrollRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public OverscrollBuilder Auto => Chain("auto");
    /// <summary>
    /// Fluent step for `Contain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder Contain => Chain("contain");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public OverscrollBuilder None => Chain("none");

    /// <summary>
    /// Fluent step for `XAuto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder XAuto => Chain("x-auto");
    /// <summary>
    /// Fluent step for `XContain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder XContain => Chain("x-contain");
    /// <summary>
    /// Fluent step for `XNone` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder XNone => Chain("x-none");

    /// <summary>
    /// Fluent step for `YAuto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder YAuto => Chain("y-auto");
    /// <summary>
    /// Fluent step for `YContain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder YContain => Chain("y-contain");
    /// <summary>
    /// Fluent step for `YNone` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public OverscrollBuilder YNone => Chain("y-none");

    /// <summary>
    /// Applies an exact Tailwind overscroll class token, e.g. "overscroll-none".
    /// </summary>
    public OverscrollBuilder Token(string token) => Chain(token);

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public OverscrollBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public OverscrollBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public OverscrollBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public OverscrollBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public OverscrollBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public OverscrollBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OverscrollBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new OverscrollRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OverscrollBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = GetOverscrollClass(rule.Value);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

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
            var style = GetOverscrollStyle(_rules[i].Value);
            if (style is null)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(style);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetOverscrollClass(string value)
    {
        return value switch
        {
            "auto" => "overscroll-auto",
            "contain" => "overscroll-contain",
            "none" => "overscroll-none",
            "x-auto" => "overscroll-x-auto",
            "x-contain" => "overscroll-x-contain",
            "x-none" => "overscroll-x-none",
            "y-auto" => "overscroll-y-auto",
            "y-contain" => "overscroll-y-contain",
            "y-none" => "overscroll-y-none",
            _ when value.StartsWith("overscroll-", StringComparison.Ordinal) => value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetOverscrollStyle(string value)
    {
        return value switch
        {
            "auto" => "overscroll-behavior: auto",
            "contain" => "overscroll-behavior: contain",
            "none" => "overscroll-behavior: none",
            "x-auto" => "overscroll-behavior-x: auto",
            "x-contain" => "overscroll-behavior-x: contain",
            "x-none" => "overscroll-behavior-x: none",
            "y-auto" => "overscroll-behavior-y: auto",
            "y-contain" => "overscroll-behavior-y: contain",
            "y-none" => "overscroll-behavior-y: none",
            _ => null
        };
    }

    public override string ToString()
    {
        return ToClass();
    }
}
