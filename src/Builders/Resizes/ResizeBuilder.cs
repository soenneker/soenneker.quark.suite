using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified resize builder with fluent API for chaining resize rules.
/// </summary>
public sealed class ResizeBuilder : ICssBuilder
{
    private readonly List<ResizeRule> _rules = new(4);

    private const string _classResizeNone = "resize-none";
    private const string _classResizeBoth = "resize-both";
    private const string _classResizeHorizontal = "resize-x";
    private const string _classResizeVertical = "resize-y";

    internal ResizeBuilder(string resize, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ResizeRule(resize, breakpoint));
    }

    internal ResizeBuilder(List<ResizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ResizeBuilder None => ChainWithResize("none");
    public ResizeBuilder Both => ChainWithResize("both");
    public ResizeBuilder Horizontal => ChainWithResize("horizontal");
    public ResizeBuilder Vertical => ChainWithResize("vertical");

    public ResizeBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ResizeBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ResizeBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ResizeBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ResizeBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ResizeBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ResizeBuilder ChainWithResize(string resize)
    {
        _rules.Add(new ResizeRule(resize, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ResizeBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ResizeRule("none", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ResizeRule last = _rules[lastIdx];
        _rules[lastIdx] = new ResizeRule(last.Resize, breakpoint);
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
            ResizeRule rule = _rules[i];
            string cls = GetResizeClass(rule.Resize);
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
            ResizeRule rule = _rules[i];
            string? resizeValue = GetResizeValue(rule.Resize);

            if (resizeValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("resize: ");
            sb.Append(resizeValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetResizeClass(string resize)
    {
        return resize switch
        {
            "none" => _classResizeNone,
            "both" => _classResizeBoth,
            "horizontal" => _classResizeHorizontal,
            "vertical" => _classResizeVertical,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetResizeValue(string resize)
    {
        return resize switch
        {
            "none" => "none",
            "both" => "both",
            "horizontal" => "horizontal",
            "vertical" => "vertical",
            _ => null
        };
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

    public override string ToString()
    {
        return ToClass();
    }
}

