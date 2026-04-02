using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Stroke line join builder. Tailwind: stroke-join-auto, stroke-join-round, stroke-join-bevel, stroke-join-miter.
/// </summary>
[TailwindPrefix("stroke-join-", Responsive = true)]
public sealed class StrokeLineJoinBuilder : ICssBuilder
{
    private readonly List<StrokeLineJoinRule> _rules = new(4);

    internal StrokeLineJoinBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeLineJoinRule(value, breakpoint));
    }

    internal StrokeLineJoinBuilder(List<StrokeLineJoinRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public StrokeLineJoinBuilder Auto => Chain("auto");
    public StrokeLineJoinBuilder Round => Chain("round");
    public StrokeLineJoinBuilder Bevel => Chain("bevel");
    public StrokeLineJoinBuilder Miter => Chain("miter");

    public StrokeLineJoinBuilder OnSm => ChainBp(BreakpointType.Sm);
    public StrokeLineJoinBuilder OnMd => ChainBp(BreakpointType.Md);
    public StrokeLineJoinBuilder OnLg => ChainBp(BreakpointType.Lg);
    public StrokeLineJoinBuilder OnXl => ChainBp(BreakpointType.Xl);
    public StrokeLineJoinBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeLineJoinBuilder Chain(string value)
    {
        _rules.Add(new StrokeLineJoinRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeLineJoinBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new StrokeLineJoinRule("auto", bp));
        else _rules[^1] = new StrokeLineJoinRule(_rules[^1].Value, bp);
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
                "auto" => "stroke-join-auto",
                "round" => "stroke-join-round",
                "bevel" => "stroke-join-bevel",
                "miter" => "stroke-join-miter",
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
            if (rule.Value is not ("auto" or "round" or "bevel" or "miter")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("stroke-linejoin: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
