using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Stroke line cap builder. Tailwind: stroke-cap-auto, stroke-cap-round, stroke-cap-square, stroke-cap-butt.
/// </summary>
[TailwindPrefix("stroke-cap-", Responsive = true)]
public sealed class StrokeLineCapBuilder : ICssBuilder
{
    private readonly List<StrokeLineCapRule> _rules = new(4);

    internal StrokeLineCapBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeLineCapRule(value, breakpoint));
    }

    internal StrokeLineCapBuilder(List<StrokeLineCapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public StrokeLineCapBuilder Auto => Chain("auto");
    public StrokeLineCapBuilder Round => Chain("round");
    public StrokeLineCapBuilder Square => Chain("square");
    public StrokeLineCapBuilder Butt => Chain("butt");

    public StrokeLineCapBuilder OnSm => ChainBp(BreakpointType.Sm);
    public StrokeLineCapBuilder OnMd => ChainBp(BreakpointType.Md);
    public StrokeLineCapBuilder OnLg => ChainBp(BreakpointType.Lg);
    public StrokeLineCapBuilder OnXl => ChainBp(BreakpointType.Xl);
    public StrokeLineCapBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeLineCapBuilder Chain(string value)
    {
        _rules.Add(new StrokeLineCapRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeLineCapBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new StrokeLineCapRule("auto", bp));
        else _rules[^1] = new StrokeLineCapRule(_rules[^1].Value, bp);
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
                "auto" => "stroke-cap-auto",
                "round" => "stroke-cap-round",
                "square" => "stroke-cap-square",
                "butt" => "stroke-cap-butt",
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
            if (rule.Value is not ("auto" or "round" or "square" or "butt")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("stroke-linecap: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
