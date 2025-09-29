using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class BoxShadowBuilder : ICssBuilder
{
    private readonly List<BoxShadowRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _classNone = "shadow-none";
    private const string _classBase = "shadow";
    private const string _classSm = "shadow-sm";
    private const string _classLg = "shadow-lg";

    internal BoxShadowBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BoxShadowRule(value, breakpoint));
    }

    internal BoxShadowBuilder(List<BoxShadowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BoxShadowBuilder None => Chain("none");
    public BoxShadowBuilder Base => Chain("base");
    public BoxShadowBuilder Small => Chain(SizeType.Small.Value);
    public BoxShadowBuilder Large => Chain(SizeType.Large.Value);

    public BoxShadowBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public BoxShadowBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public BoxShadowBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public BoxShadowBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public BoxShadowBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public BoxShadowBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder Chain(string value)
    {
        _rules.Add(new BoxShadowRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "base" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BoxShadowRule("base", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BoxShadowRule(last.Value, bp);
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

            var baseClass = rule.Value switch
            {
                "none" => _classNone,
                "base" => _classBase,
                "sm" => _classSm,
                "lg" => _classLg,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                baseClass = InsertBreakpointType(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        // Shadow utilities are class-first; no inline style mapping
        return string.Empty;
    }


    /// <summary>
    /// Insert BreakpointType token as: "shadow-lg" + "md" → "shadow-md-lg".
    /// Falls back to "bp-{class}" if no dash exists.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            // length = prefix + "-" + bp + remainder
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                // prefix
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;

                // "-" + bp
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;

                // remainder (starts with '-')
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        // Fallback: "bp-{className}"
        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}
