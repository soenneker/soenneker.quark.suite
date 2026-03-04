using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap type builder. Tailwind: snap-none, snap-x, snap-y, snap-both, snap-mandatory, snap-proximity.
/// </summary>
[TailwindPrefix("snap-", Responsive = true)]
public sealed class ScrollSnapBuilder : ICssBuilder
{
    private readonly List<ScrollSnapRule> _rules = new(4);

    internal ScrollSnapBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapRule(value, breakpoint));
    }

    internal ScrollSnapBuilder(List<ScrollSnapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScrollSnapBuilder None => Chain("none");
    public ScrollSnapBuilder X => Chain("x");
    public ScrollSnapBuilder Y => Chain("y");
    public ScrollSnapBuilder Both => Chain("both");
    public ScrollSnapBuilder Mandatory => Chain("mandatory");
    public ScrollSnapBuilder Proximity => Chain("proximity");

    public ScrollSnapBuilder OnSm => ChainBp(BreakpointType.Sm);
    public ScrollSnapBuilder OnMd => ChainBp(BreakpointType.Md);
    public ScrollSnapBuilder OnLg => ChainBp(BreakpointType.Lg);
    public ScrollSnapBuilder OnXl => ChainBp(BreakpointType.Xl);
    public ScrollSnapBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapBuilder Chain(string value)
    {
        _rules.Add(new ScrollSnapRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new ScrollSnapRule("none", bp));
        else _rules[^1] = new ScrollSnapRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch
            {
                "none" => "snap-none",
                "x" => "snap-x",
                "y" => "snap-y",
                "both" => "snap-both",
                "mandatory" => "snap-mandatory",
                "proximity" => "snap-proximity",
                _ => string.Empty
            };
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
        }
        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var styleVal = rule.Value switch
            {
                "none" => "none",
                "x" => "x mandatory",
                "y" => "y mandatory",
                "both" => "both mandatory",
                "mandatory" => "both mandatory",
                "proximity" => "both proximity",
                _ => null
            };
            if (styleVal is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("scroll-snap-type: ");
            sb.Append(styleVal);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
