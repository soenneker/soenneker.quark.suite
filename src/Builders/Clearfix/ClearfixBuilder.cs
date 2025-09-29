using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified clearfix builder with fluent API for chaining clearfix rules.
/// </summary>
public sealed class ClearfixBuilder : ICssBuilder
{
    private readonly List<ClearfixRule> _rules = new(4);

    private const string _classClearfix = "clearfix";

    internal ClearfixBuilder(BreakpointType? breakpoint = null)
    {
        _rules.Add(new ClearfixRule(breakpoint));
    }

    internal ClearfixBuilder(List<ClearfixRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ClearfixBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ClearfixBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ClearfixBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ClearfixBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ClearfixBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ClearfixBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ClearfixBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ClearfixRule(breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ClearfixRule(breakpoint);
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
            var cls = _classClearfix;

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
            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("&::after { display: block; clear: both; content: \"\"; }");
        }

        return sb.ToString();
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

