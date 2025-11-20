using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class VerticalAlignBuilder : ICssBuilder
{
    private readonly List<VerticalAlignRule> _rules = new(6);

    internal VerticalAlignBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new VerticalAlignRule(value, breakpoint));
    }

    internal VerticalAlignBuilder(List<VerticalAlignRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public VerticalAlignBuilder Baseline => Chain(VerticalAlignKeyword.BaselineValue);
    public VerticalAlignBuilder Top => Chain(VerticalAlignKeyword.TopValue);
    public VerticalAlignBuilder Middle => Chain(VerticalAlignKeyword.MiddleValue);
    public VerticalAlignBuilder Bottom => Chain(VerticalAlignKeyword.BottomValue);
    public VerticalAlignBuilder TextTop => Chain(VerticalAlignKeyword.TextTopValue);
    public VerticalAlignBuilder TextBottom => Chain(VerticalAlignKeyword.TextBottomValue);
    public VerticalAlignBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public VerticalAlignBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public VerticalAlignBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public VerticalAlignBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public VerticalAlignBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public VerticalAlignBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public VerticalAlignBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public VerticalAlignBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public VerticalAlignBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public VerticalAlignBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public VerticalAlignBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder Chain(string value)
    {
        _rules.Add(new VerticalAlignRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new VerticalAlignRule(VerticalAlignKeyword.BaselineValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new VerticalAlignRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                VerticalAlignKeyword.BaselineValue => "align-baseline",
                VerticalAlignKeyword.TopValue => "align-top",
                VerticalAlignKeyword.MiddleValue => "align-middle",
                VerticalAlignKeyword.BottomValue => "align-bottom",
                VerticalAlignKeyword.TextTopValue => "align-text-top",
                VerticalAlignKeyword.TextBottomValue => "align-text-bottom",
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = rule.Value;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("vertical-align: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
