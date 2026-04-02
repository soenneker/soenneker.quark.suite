using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance ring color builder.
/// Produces ring color utility classes.
/// </summary>
[TailwindPrefix("ring-", Responsive = true)]
public sealed class RingColorBuilder : ICssBuilder
{
    private readonly List<RingColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal RingColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new RingColorRule(value, breakpoint));
    }

    internal RingColorBuilder(List<RingColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Primary` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Primary => ChainValue("primary");
    /// <summary>
    /// Fluent step for `Primary Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder PrimaryForeground => ChainValue("primary-foreground");
    /// <summary>
    /// Fluent step for `Secondary` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Secondary => ChainValue("secondary");
    /// <summary>
    /// Fluent step for `Secondary Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
    /// <summary>
    /// Fluent step for `Destructive` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Destructive => ChainValue("destructive");
    /// <summary>
    /// Fluent step for `Destructive Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
    /// <summary>
    /// Fluent step for `Muted` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Muted => ChainValue("muted");
    /// <summary>
    /// Fluent step for `Muted Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder MutedForeground => ChainValue("muted-foreground");
    /// <summary>
    /// Fluent step for `Accent` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Accent => ChainValue("accent");
    /// <summary>
    /// Fluent step for `Accent Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder AccentForeground => ChainValue("accent-foreground");
    /// <summary>
    /// Fluent step for `Popover` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Popover => ChainValue("popover");
    /// <summary>
    /// Fluent step for `Popover Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder PopoverForeground => ChainValue("popover-foreground");
    /// <summary>
    /// Fluent step for `Card` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Card => ChainValue("card");
    /// <summary>
    /// Fluent step for `Card Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder CardForeground => ChainValue("card-foreground");
    /// <summary>
    /// Fluent step for `Background` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Background => ChainValue("background");
    /// <summary>
    /// Fluent step for `Foreground` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Foreground => ChainValue("foreground");
    /// <summary>
    /// Fluent step for `Border` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Border => ChainValue("border");
    /// <summary>
    /// Fluent step for `Input` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Input => ChainValue("input");
    /// <summary>
    /// Fluent step for `Ring` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Ring => ChainValue("ring");
    /// <summary>
    /// Fluent step for `Success` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Success => ChainValue("success");
    /// <summary>
    /// Fluent step for `Warning` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Warning => ChainValue("warning");
    /// <summary>
    /// Fluent step for `Info` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Info => ChainValue("info");
    /// <summary>
    /// Fluent step for `White` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder White => ChainValue("white");
    /// <summary>
    /// Fluent step for `Black` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public RingColorBuilder Black => ChainValue("black");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public RingColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public RingColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public RingColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public RingColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public RingColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public RingColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingColorBuilder ChainValue(string value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new RingColorRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingColorBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

            var breakpoint = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (breakpoint.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, breakpoint);

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
            var rule = _rules[i];
            var css = GetStyle(rule);
            if (css is null)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        var classResult = ToClass();
        if (classResult.HasContent())
            return classResult;

        return ToStyle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(RingColorRule rule)
    {
        return rule.Value switch
        {
            "primary" => "ring-primary",
            "primary-foreground" => "ring-primary-foreground",
            "secondary" => "ring-secondary",
            "secondary-foreground" => "ring-secondary-foreground",
            "destructive" => "ring-destructive",
            "destructive-foreground" => "ring-destructive-foreground",
            "muted" => "ring-muted",
            "muted-foreground" => "ring-muted-foreground",
            "accent" => "ring-accent",
            "accent-foreground" => "ring-accent-foreground",
            "popover" => "ring-popover",
            "popover-foreground" => "ring-popover-foreground",
            "card" => "ring-card",
            "card-foreground" => "ring-card-foreground",
            "background" => "ring-background",
            "foreground" => "ring-foreground",
            "border" => "ring-border",
            "input" => "ring-input",
            "ring" => "ring-ring",
            "success" => "ring-success",
            "warning" => "ring-warning",
            "info" => "ring-info",
            "white" => "ring-white",
            "black" => "ring-black",
            _ when rule.Value.StartsWith("ring-") => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(RingColorRule rule)
    {
        return GetClass(rule).Length != 0 ? null : rule.Value;
    }
}
