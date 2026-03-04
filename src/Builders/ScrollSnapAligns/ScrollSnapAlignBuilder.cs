using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap align builder. Tailwind: snap-start, snap-center, snap-end, snap-align-none.
/// </summary>
[TailwindPrefix("snap-", Responsive = true)]
public sealed class ScrollSnapAlignBuilder : ICssBuilder
{
    private readonly List<ScrollSnapAlignRule> _rules = new(4);

    internal ScrollSnapAlignBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapAlignRule(value, breakpoint));
    }

    internal ScrollSnapAlignBuilder(List<ScrollSnapAlignRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScrollSnapAlignBuilder Start => Chain("start");
    public ScrollSnapAlignBuilder Center => Chain("center");
    public ScrollSnapAlignBuilder End => Chain("end");
    public ScrollSnapAlignBuilder None => Chain("none");

    public ScrollSnapAlignBuilder OnSm => ChainBp(BreakpointType.Sm);
    public ScrollSnapAlignBuilder OnMd => ChainBp(BreakpointType.Md);
    public ScrollSnapAlignBuilder OnLg => ChainBp(BreakpointType.Lg);
    public ScrollSnapAlignBuilder OnXl => ChainBp(BreakpointType.Xl);
    public ScrollSnapAlignBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapAlignBuilder Chain(string value)
    {
        _rules.Add(new ScrollSnapAlignRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapAlignBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new ScrollSnapAlignRule("start", bp));
        else _rules[^1] = new ScrollSnapAlignRule(_rules[^1].Value, bp);
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
                "start" => "snap-start",
                "center" => "snap-center",
                "end" => "snap-end",
                "none" => "snap-align-none",
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
            if (rule.Value is not ("start" or "center" or "end" or "none")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("scroll-snap-align: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
