using Soenneker.Quark.Attributes;
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

    /// <summary>
    /// Fluent step for `Evenodd` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FillRuleBuilder Evenodd => Chain("evenodd");
    /// <summary>
    /// Fluent step for `Nonzero` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FillRuleBuilder Nonzero => Chain("nonzero");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public FillRuleBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public FillRuleBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public FillRuleBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public FillRuleBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
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
