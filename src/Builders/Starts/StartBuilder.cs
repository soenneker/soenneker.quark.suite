using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Start (inset-inline-start) builder. Tailwind: start-*.
/// </summary>
public sealed class StartBuilder : ICssBuilder
{
    private readonly List<StartRule> _rules = new(4);

    internal StartBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StartRule(value, breakpoint));
    }

    internal StartBuilder(List<StartRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public StartBuilder Is0 => Chain(ScaleType.Is0Value);
    public StartBuilder Is1 => Chain(ScaleType.Is1Value);
    public StartBuilder Is2 => Chain(ScaleType.Is2Value);
    public StartBuilder Is3 => Chain(ScaleType.Is3Value);
    public StartBuilder Is4 => Chain(ScaleType.Is4Value);
    public StartBuilder Is5 => Chain(ScaleType.Is5Value);
    public StartBuilder Auto => Chain("auto");
    public StartBuilder Px => Chain("px");

    public StartBuilder OnSm => ChainBp(BreakpointType.Sm);
    public StartBuilder OnMd => ChainBp(BreakpointType.Md);
    public StartBuilder OnLg => ChainBp(BreakpointType.Lg);
    public StartBuilder OnXl => ChainBp(BreakpointType.Xl);
    public StartBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StartBuilder Chain(string value)
    {
        _rules.Add(new StartRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StartBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new StartRule(ScaleType.Is0Value, bp));
        else _rules[^1] = new StartRule(_rules[^1].Value, bp);
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
            sb.Append("inset-inline-start: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v) => v switch
    {
        ScaleType.Is0Value => "start-0",
        ScaleType.Is1Value => "start-1",
        ScaleType.Is2Value => "start-2",
        ScaleType.Is3Value => "start-3",
        ScaleType.Is4Value => "start-4",
        ScaleType.Is5Value => "start-5",
        "auto" => "start-auto",
        "px" => "start-px",
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
