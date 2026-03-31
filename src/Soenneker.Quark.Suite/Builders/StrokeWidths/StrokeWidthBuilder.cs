using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Stroke width builder for SVG/CSS. Tailwind: stroke-0, stroke-1, stroke-2.
/// </summary>
[TailwindPrefix("stroke-", Responsive = true)]
public sealed class StrokeWidthBuilder : ICssBuilder
{
    private readonly List<StrokeWidthRule> _rules = new(4);

    internal StrokeWidthBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeWidthRule(value, breakpoint));
    }

    internal StrokeWidthBuilder(List<StrokeWidthRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public StrokeWidthBuilder Is0 => Chain("0");
    public StrokeWidthBuilder Is1 => Chain("1");
    public StrokeWidthBuilder Is2 => Chain("2");

    public StrokeWidthBuilder OnSm => ChainBp(BreakpointType.Sm);
    public StrokeWidthBuilder OnMd => ChainBp(BreakpointType.Md);
    public StrokeWidthBuilder OnLg => ChainBp(BreakpointType.Lg);
    public StrokeWidthBuilder OnXl => ChainBp(BreakpointType.Xl);
    public StrokeWidthBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeWidthBuilder Chain(string value)
    {
        _rules.Add(new StrokeWidthRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeWidthBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new StrokeWidthRule("0", bp));
        else _rules[^1] = new StrokeWidthRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch { "0" => "stroke-0", "1" => "stroke-1", "2" => "stroke-2", _ => string.Empty };
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
            var val = rule.Value switch { "0" => "0", "1" => "1", "2" => "2", _ => null };
            if (val is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("stroke-width: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
