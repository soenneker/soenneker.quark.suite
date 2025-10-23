using Soenneker.Quark.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap list variant classes with fluent API for chaining rules.
/// </summary>
public sealed class ListVariantBuilder : ICssBuilder
{
    private readonly List<ListVariantRule> _rules = new(4);

    private const string _classUnstyled = "list-unstyled";
    private const string _classInline = "list-inline";
    private const string _classInlineItem = "list-inline-item";

    internal ListVariantBuilder(ListVariantType type, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ListVariantRule(type, breakpoint));
    }

    internal ListVariantBuilder(List<ListVariantRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ListVariantBuilder Unstyled => Chain(ListVariantType.Unstyled);
    public ListVariantBuilder Inline => Chain(ListVariantType.Inline);
    public ListVariantBuilder InlineItem => Chain(ListVariantType.InlineItem);

    public ListVariantBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ListVariantBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ListVariantBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ListVariantBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ListVariantBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ListVariantBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ListVariantBuilder Chain(ListVariantType type)
    {
        _rules.Add(new ListVariantRule(type, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ListVariantBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ListVariantRule(ListVariantType.Unstyled, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ListVariantRule(last.Type, breakpoint);
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
            var typeValue = rule.Type.Value;
            var cls = typeValue switch
            {
                "unstyled" => _classUnstyled,
                "inline" => _classInline,
                "inline-item" => _classInlineItem,
                _ => string.Empty
            };

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
            var typeValue = rule.Type.Value;
            var style = typeValue switch
            {
                "unstyled" => "list-style: none; padding-left: 0;",
                "inline" => "padding-left: 0; list-style: none;",
                "inline-item" => "display: inline-block;",
                _ => null
            };

            if (style == null)
                continue;

            if (!first) sb.Append(" ");
            else first = false;

            sb.Append(style);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            // Insert breakpoint after first dash: "list-unstyled" -> "list-md-unstyled"
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                // prefix (e.g., "list")
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;

                // "-" + bp
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;

                // remainder (starts with '-', e.g., "-unstyled")
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

    public override string ToString()
    {
        return ToClass();
    }
}


