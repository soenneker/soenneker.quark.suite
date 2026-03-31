using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// SVG fill-rule builder. Tailwind: fill-rule-evenodd, fill-rule-nonzero.
/// </summary>
[TailwindPrefix("fill-rule-", Responsive = true)]
public sealed class FillRuleBuilder : ICssBuilder
{
    private readonly List<FillRuleRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

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

    public FillRuleBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public FillRuleBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public FillRuleBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public FillRuleBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public FillRuleBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillRuleBuilder Chain(string value)
    {
        _rules.Add(new FillRuleRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillRuleBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
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
