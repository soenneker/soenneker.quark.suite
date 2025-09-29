using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified flex builder with fluent API for chaining flex rules.
/// </summary>
public sealed class FlexBuilder : ICssBuilder
{
    private readonly List<FlexRule> _rules = new(8);

    internal FlexBuilder(string property, string? value = null, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FlexRule(property, value ?? string.Empty, breakpoint));
    }

    internal FlexBuilder(List<FlexRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Display properties
    /// <summary>
    /// Chain with display flex for the next rule.
    /// </summary>
    public FlexBuilder Display => ChainWithRule("display", string.Empty);

    // Direction properties
    /// <summary>
    /// Chain with flex direction row for the next rule.
    /// </summary>
    public FlexBuilder Row => ChainWithRule("direction", "row");

    /// <summary>
    /// Chain with flex direction column for the next rule.
    /// </summary>
    public FlexBuilder Column => ChainWithRule("direction", "column");

    // Wrap properties
    /// <summary>
    /// Chain with flex wrap for the next rule.
    /// </summary>
    public FlexBuilder Wrap => ChainWithRule("wrap", "wrap");

    /// <summary>
    /// Chain with flex nowrap for the next rule.
    /// </summary>
    public FlexBuilder NoWrap => ChainWithRule("wrap", "nowrap");

    // Justify content properties
    /// <summary>
    /// Chain with justify content start for the next rule.
    /// </summary>
    public FlexBuilder JustifyStart => ChainWithRule("justify", "start");

    /// <summary>
    /// Chain with justify content end for the next rule.
    /// </summary>
    public FlexBuilder JustifyEnd => ChainWithRule("justify", "end");

    /// <summary>
    /// Chain with justify content center for the next rule.
    /// </summary>
    public FlexBuilder JustifyCenter => ChainWithRule("justify", "center");

    /// <summary>
    /// Chain with justify content between for the next rule.
    /// </summary>
    public FlexBuilder JustifyBetween => ChainWithRule("justify", "between");

    /// <summary>
    /// Chain with justify content around for the next rule.
    /// </summary>
    public FlexBuilder JustifyAround => ChainWithRule("justify", "around");

    /// <summary>
    /// Chain with justify content evenly for the next rule.
    /// </summary>
    public FlexBuilder JustifyEvenly => ChainWithRule("justify", "evenly");

    // Align items properties
    /// <summary>
    /// Chain with align items start for the next rule.
    /// </summary>
    public FlexBuilder AlignStart => ChainWithRule("align", "start");

    /// <summary>
    /// Chain with align items end for the next rule.
    /// </summary>
    public FlexBuilder AlignEnd => ChainWithRule("align", "end");

    /// <summary>
    /// Chain with align items center for the next rule.
    /// </summary>
    public FlexBuilder AlignCenter => ChainWithRule("align", "center");

    /// <summary>
    /// Chain with align items baseline for the next rule.
    /// </summary>
    public FlexBuilder AlignBaseline => ChainWithRule("align", "baseline");

    /// <summary>
    /// Chain with align items stretch for the next rule.
    /// </summary>
    public FlexBuilder AlignStretch => ChainWithRule("align", "stretch");

    /// <summary>
    /// Apply on phone devices (portrait phones, less than 576px).
    /// </summary>
    public FlexBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);

    /// <summary>
    /// Apply on tablet devices (tablets, 768px and up).
    /// </summary>
    public FlexBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);

    /// <summary>
    /// Apply on laptop devices (laptops, 992px and up).
    /// </summary>
    public FlexBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);

    /// <summary>
    /// Apply on desktop devices (desktops, 1200px and up).
    /// </summary>
    public FlexBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);

    /// <summary>
    /// Apply on wide screen devices (larger desktops, 1400px and up).
    /// </summary>
    public FlexBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);

    public FlexBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FlexBuilder ChainWithRule(string property, string value)
    {
        _rules.Add(new FlexRule(property, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FlexBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FlexRule("display", string.Empty, breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        FlexRule last = _rules[lastIdx];
        _rules[lastIdx] = new FlexRule(last.Property, last.Value, breakpoint);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            FlexRule rule = _rules[i];
            string cls = GetFlexClass(rule.Property, rule.Value);
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

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            FlexRule rule = _rules[i];
            string? css = GetCssProperty(rule.Property, rule.Value);
            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    private static string GetFlexClass(string property, string value)
    {
        return property switch
        {
            "display" => "d-flex",
            "direction" => value switch
            {
                "row" => "flex-row",
                "column" => "flex-column",
                _ => string.Empty
            },
            "wrap" => value switch
            {
                "wrap" => "flex-wrap",
                "nowrap" => "flex-nowrap",
                _ => string.Empty
            },
            "justify" => value switch
            {
                "start" => "justify-content-start",
                "end" => "justify-content-end",
                "center" => "justify-content-center",
                "between" => "justify-content-between",
                "around" => "justify-content-around",
                "evenly" => "justify-content-evenly",
                _ => string.Empty
            },
            "align" => value switch
            {
                "start" => "align-items-start",
                "end" => "align-items-end",
                "center" => "align-items-center",
                "baseline" => "align-items-baseline",
                "stretch" => "align-items-stretch",
                _ => string.Empty
            },
            _ => string.Empty
        };
    }

    private static string? GetCssProperty(string property, string value)
    {
        return property switch
        {
            "display" => "display: flex",
            "direction" => value switch
            {
                "row" => "flex-direction: row",
                "column" => "flex-direction: column",
                _ => null
            },
            "wrap" => value switch
            {
                "wrap" => "flex-wrap: wrap",
                "nowrap" => "flex-wrap: nowrap",
                _ => null
            },
            "justify" => value switch
            {
                "start" => "justify-content: flex-start",
                "end" => "justify-content: flex-end",
                "center" => "justify-content: center",
                "between" => "justify-content: space-between",
                "around" => "justify-content: space-around",
                "evenly" => "justify-content: space-evenly",
                _ => null
            },
            "align" => value switch
            {
                "start" => "align-items: flex-start",
                "end" => "align-items: flex-end",
                "center" => "align-items: center",
                "baseline" => "align-items: baseline",
                "stretch" => "align-items: stretch",
                _ => null
            },
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
}

