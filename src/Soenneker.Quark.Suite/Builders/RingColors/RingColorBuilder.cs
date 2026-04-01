using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
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

    public RingColorBuilder Primary => ChainValue("primary");
    public RingColorBuilder PrimaryForeground => ChainValue("primary-foreground");
    public RingColorBuilder Secondary => ChainValue("secondary");
    public RingColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
    public RingColorBuilder Destructive => ChainValue("destructive");
    public RingColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
    public RingColorBuilder Muted => ChainValue("muted");
    public RingColorBuilder MutedForeground => ChainValue("muted-foreground");
    public RingColorBuilder Accent => ChainValue("accent");
    public RingColorBuilder AccentForeground => ChainValue("accent-foreground");
    public RingColorBuilder Popover => ChainValue("popover");
    public RingColorBuilder PopoverForeground => ChainValue("popover-foreground");
    public RingColorBuilder Card => ChainValue("card");
    public RingColorBuilder CardForeground => ChainValue("card-foreground");
    public RingColorBuilder Background => ChainValue("background");
    public RingColorBuilder Foreground => ChainValue("foreground");
    public RingColorBuilder Border => ChainValue("border");
    public RingColorBuilder Input => ChainValue("input");
    public RingColorBuilder Ring => ChainValue("ring");
    public RingColorBuilder Success => ChainValue("success");
    public RingColorBuilder Warning => ChainValue("warning");
    public RingColorBuilder Info => ChainValue("info");
    public RingColorBuilder White => ChainValue("white");
    public RingColorBuilder Black => ChainValue("black");

    public RingColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public RingColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public RingColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public RingColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public RingColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
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
