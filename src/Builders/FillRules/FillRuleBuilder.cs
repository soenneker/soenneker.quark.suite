using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// SVG fill-rule builder. Tailwind: fill-rule-evenodd, fill-rule-nonzero.
/// </summary>
public sealed class FillRuleBuilder : ICssBuilder
{
    private readonly List<FillRuleRule> _rules = new(4);

    internal FillRuleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FillRuleRule(value, breakpoint));
    }

    internal FillRuleBuilder(List<FillRuleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public FillRuleBuilder Evenodd => Chain("evenodd");
    public FillRuleBuilder Nonzero => Chain("nonzero");

    public FillRuleBuilder OnSm => ChainBp(BreakpointType.Sm);
    public FillRuleBuilder OnMd => ChainBp(BreakpointType.Md);
    public FillRuleBuilder OnLg => ChainBp(BreakpointType.Lg);
    public FillRuleBuilder OnXl => ChainBp(BreakpointType.Xl);
    public FillRuleBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillRuleBuilder Chain(string value)
    {
        _rules.Add(new FillRuleRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillRuleBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new FillRuleRule("nonzero", bp));
        else _rules[^1] = new FillRuleRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch { "evenodd" => "fill-rule-evenodd", "nonzero" => "fill-rule-nonzero", _ => string.Empty };
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
            if (rule.Value is not ("evenodd" or "nonzero")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("fill-rule: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
