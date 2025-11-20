using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class TextWrapBuilder : ICssBuilder
{
    private readonly List<TextWrapRule> _rules = new(4);

    private const string _classWrap = "text-wrap";
    private const string _classNoWrap = "text-nowrap";
    private const string _stylePrefix = "text-wrap: ";

    internal TextWrapBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextWrapRule(value, breakpoint));
    }

    internal TextWrapBuilder(List<TextWrapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextWrapBuilder Wrap => Chain(TextWrapKeyword.WrapValue);
    public TextWrapBuilder NoWrap => Chain(TextWrapKeyword.NoWrapValue);
    public TextWrapBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextWrapBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextWrapBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextWrapBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextWrapBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public TextWrapBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextWrapBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextWrapBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextWrapBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextWrapBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextWrapBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextWrapBuilder Chain(string value)
    {
        _rules.Add(new TextWrapRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextWrapBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextWrapRule(TextWrapKeyword.WrapValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextWrapRule(last.Value, bp);
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
                TextWrapKeyword.WrapValue => _classWrap,
                TextWrapKeyword.NoWrapValue => _classNoWrap,
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
            var css = rule.Value switch
            {
                TextWrapKeyword.WrapValue => "wrap",
                TextWrapKeyword.NoWrapValue => "nowrap",
                GlobalKeyword.InheritValue => "inherit",
                GlobalKeyword.InitialValue => "initial",
                GlobalKeyword.UnsetValue => "unset",
                GlobalKeyword.RevertValue => "revert",
                GlobalKeyword.RevertLayerValue => "revert-layer",
                _ => null
            };

            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
