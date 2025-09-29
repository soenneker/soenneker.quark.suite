using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class TextTransformBuilder : ICssBuilder
{
    private readonly List<TextTransformRule> _rules = new(4);

    private const string _classLower = "text-lowercase";
    private const string _classUpper = "text-uppercase";
    private const string _classCap = "text-capitalize";
    private const string _stylePrefix = "text-transform: ";

    internal TextTransformBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextTransformRule(value, breakpoint));
    }

    internal TextTransformBuilder(List<TextTransformRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextTransformBuilder Lowercase => Chain(TextTransformKeyword.LowercaseValue);
    public TextTransformBuilder Uppercase => Chain(TextTransformKeyword.UppercaseValue);
    public TextTransformBuilder Capitalize => Chain(TextTransformKeyword.CapitalizeValue);
    public TextTransformBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextTransformBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextTransformBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextTransformBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextTransformBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public TextTransformBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextTransformBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextTransformBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextTransformBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextTransformBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextTransformBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder Chain(string value)
    {
        _rules.Add(new TextTransformRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextTransformRule(TextTransformKeyword.LowercaseValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextTransformRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextTransformRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            TextTransformRule rule = _rules[i];
            string cls = rule.Value switch
            {
                TextTransformKeyword.LowercaseValue => _classLower,
                TextTransformKeyword.UppercaseValue => _classUpper,
                TextTransformKeyword.CapitalizeValue => _classCap,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

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
            TextTransformRule rule = _rules[i];
            string val = rule.Value;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(val);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}

 
