using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Builders.Abstract;
using Soenneker.Quark.Builders.Utils;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark.Builders.Floats;

/// <summary>
/// Simplified float builder with fluent API for chaining float rules.
/// </summary>
public sealed class FloatBuilder : ICssBuilder
{
    private readonly List<FloatRule> _rules = new(4);

    private const string _classStart = "float-start";
    private const string _classEnd = "float-end";
    private const string _classNone = "float-none";

    private const string _floatPrefix = "float: ";

    internal FloatBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FloatRule(value, breakpoint));
    }

    internal FloatBuilder(List<FloatRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public FloatBuilder None => ChainWithValue(FloatKeyword.NoneValue);
    public FloatBuilder Left => ChainWithValue(FloatKeyword.LeftValue);
    public FloatBuilder Right => ChainWithValue(FloatKeyword.RightValue);
    public FloatBuilder Start => ChainWithValue(FloatKeyword.InlineStartValue);
    public FloatBuilder End => ChainWithValue(FloatKeyword.InlineEndValue);
    public FloatBuilder Inherit => ChainWithValue(GlobalKeyword.InheritValue);
    public FloatBuilder Initial => ChainWithValue(GlobalKeyword.InitialValue);
    public FloatBuilder Revert => ChainWithValue(GlobalKeyword.RevertValue);
    public FloatBuilder RevertLayer => ChainWithValue(GlobalKeyword.RevertLayerValue);
    public FloatBuilder Unset => ChainWithValue(GlobalKeyword.UnsetValue);

    public FloatBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public FloatBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public FloatBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public FloatBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public FloatBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public FloatBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder ChainWithValue(string value)
    {
        _rules.Add(new FloatRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FloatRule(FloatKeyword.NoneValue, breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        FloatRule last = _rules[lastIdx];
        _rules[lastIdx] = new FloatRule(last.Value, breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            FloatRule rule = _rules[i];

            string cls = rule.Value switch
            {
                FloatKeyword.LeftValue => _classStart,
                FloatKeyword.RightValue => _classEnd,
                FloatKeyword.InlineStartValue => _classStart,
                FloatKeyword.InlineEndValue => _classEnd,
                FloatKeyword.NoneValue => _classNone,
                _ => string.Empty
            };

            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            FloatRule rule = _rules[i];

            string? css = rule.Value switch
            {
                FloatKeyword.LeftValue => "left",
                FloatKeyword.RightValue => "right",
                FloatKeyword.InlineStartValue => "inline-start",
                FloatKeyword.InlineEndValue => "inline-end",
                FloatKeyword.NoneValue => "none",
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

            sb.Append(_floatPrefix);
            sb.Append(css);
        }

        return sb.ToString();
    }


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
