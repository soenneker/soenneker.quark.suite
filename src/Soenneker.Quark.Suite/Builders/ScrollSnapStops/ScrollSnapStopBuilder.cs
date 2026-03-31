using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap stop builder. Tailwind: snap-stop-normal, snap-stop-always.
/// </summary>
[TailwindPrefix("snap-stop-", Responsive = true)]
public sealed class ScrollSnapStopBuilder : ICssBuilder
{
    private readonly List<ScrollSnapStopRule> _rules = new(4);

    internal ScrollSnapStopBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapStopRule(value, breakpoint));
    }

    internal ScrollSnapStopBuilder(List<ScrollSnapStopRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScrollSnapStopBuilder Normal => Chain("normal");
    public ScrollSnapStopBuilder Always => Chain("always");

    public ScrollSnapStopBuilder OnSm => ChainBp(BreakpointType.Sm);
    public ScrollSnapStopBuilder OnMd => ChainBp(BreakpointType.Md);
    public ScrollSnapStopBuilder OnLg => ChainBp(BreakpointType.Lg);
    public ScrollSnapStopBuilder OnXl => ChainBp(BreakpointType.Xl);
    public ScrollSnapStopBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapStopBuilder Chain(string value)
    {
        _rules.Add(new ScrollSnapStopRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapStopBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new ScrollSnapStopRule("normal", bp));
        else _rules[^1] = new ScrollSnapStopRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch { "normal" => "snap-stop-normal", "always" => "snap-stop-always", _ => string.Empty };
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
            if (rule.Value is not ("normal" or "always")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("scroll-snap-stop: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
