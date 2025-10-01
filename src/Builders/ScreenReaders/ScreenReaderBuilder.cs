using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified screen reader builder with fluent API for chaining screen reader rules.
/// </summary>
public sealed class ScreenReaderBuilder : ICssBuilder
{
    private readonly List<ScreenReaderRule> _rules = new(4);

    private const string _classSrOnly = "sr-only";
    private const string _classSrOnlyFocusable = "sr-only-focusable";

    internal ScreenReaderBuilder(string type, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScreenReaderRule(type, breakpoint));
    }

    internal ScreenReaderBuilder(List<ScreenReaderRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScreenReaderBuilder Only => ChainWithType("only");
    public ScreenReaderBuilder OnlyFocusable => ChainWithType("only-focusable");

    public ScreenReaderBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ScreenReaderBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ScreenReaderBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ScreenReaderBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ScreenReaderBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ScreenReaderBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScreenReaderBuilder ChainWithType(string type)
    {
        _rules.Add(new ScreenReaderRule(type, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScreenReaderBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ScreenReaderRule("only", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ScreenReaderRule(last.Type, breakpoint);
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
            var rule = _rules[i];
            var cls = GetScreenReaderClass(rule.Type);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
            var rule = _rules[i];
            var styleValue = GetScreenReaderStyle(rule.Type);

            if (styleValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(styleValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetScreenReaderClass(string type)
    {
        return type switch
        {
            "only" => _classSrOnly,
            "only-focusable" => _classSrOnlyFocusable,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetScreenReaderStyle(string type)
    {
        return type switch
        {
            "only" => "position: absolute; width: 1px; height: 1px; padding: 0; margin: -1px; overflow: hidden; clip: rect(0, 0, 0, 0); white-space: nowrap; border: 0",
            "only-focusable" => "position: absolute; width: 1px; height: 1px; padding: 0; margin: -1px; overflow: hidden; clip: rect(0, 0, 0, 0); white-space: nowrap; border: 0; &:focus { position: static; width: auto; height: auto; padding: inherit; margin: inherit; overflow: visible; clip: auto; white-space: normal; }",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }

    public override string ToString()
    {
        return ToClass();
    }
}

