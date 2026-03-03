using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// End (inset-inline-end) builder. Tailwind: end-*.
/// </summary>
public sealed class EndBuilder : ICssBuilder
{
    private readonly List<EndRule> _rules = new(4);

    internal EndBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new EndRule(value, breakpoint));
    }

    internal EndBuilder(List<EndRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public EndBuilder Is0 => Chain(ScaleType.Is0Value);
    public EndBuilder Is1 => Chain(ScaleType.Is1Value);
    public EndBuilder Is2 => Chain(ScaleType.Is2Value);
    public EndBuilder Is3 => Chain(ScaleType.Is3Value);
    public EndBuilder Is4 => Chain(ScaleType.Is4Value);
    public EndBuilder Is5 => Chain(ScaleType.Is5Value);
    public EndBuilder Auto => Chain("auto");
    public EndBuilder Px => Chain("px");

    public EndBuilder OnSm => ChainBp(BreakpointType.Sm);
    public EndBuilder OnMd => ChainBp(BreakpointType.Md);
    public EndBuilder OnLg => ChainBp(BreakpointType.Lg);
    public EndBuilder OnXl => ChainBp(BreakpointType.Xl);
    public EndBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private EndBuilder Chain(string value)
    {
        _rules.Add(new EndRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private EndBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new EndRule(ScaleType.Is0Value, bp));
        else _rules[^1] = new EndRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = GetClass(rule.Value);
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
            var val = GetStyleValue(rule.Value);
            if (val is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("inset-inline-end: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v) => v switch
    {
        ScaleType.Is0Value => "end-0",
        ScaleType.Is1Value => "end-1",
        ScaleType.Is2Value => "end-2",
        ScaleType.Is3Value => "end-3",
        ScaleType.Is4Value => "end-4",
        ScaleType.Is5Value => "end-5",
        "auto" => "end-auto",
        "px" => "end-px",
        _ => string.Empty
    };

    private static string? GetStyleValue(string v) => v switch
    {
        ScaleType.Is0Value => "0",
        ScaleType.Is1Value => "0.25rem",
        ScaleType.Is2Value => "0.5rem",
        ScaleType.Is3Value => "1rem",
        ScaleType.Is4Value => "1.5rem",
        ScaleType.Is5Value => "3rem",
        "auto" => "auto",
        "px" => "1px",
        _ => null
    };
}
