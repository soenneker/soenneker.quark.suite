using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
    /// Chain with flex direction row-reverse for the next rule.
    /// </summary>
    public FlexBuilder RowReverse => ChainWithRule("direction", "row-reverse");

    /// <summary>
    /// Chain with flex direction column for the next rule.
    /// </summary>
    public FlexBuilder Column => ChainWithRule("direction", "column");

    /// <summary>
    /// Chain with flex direction column-reverse for the next rule.
    /// </summary>
    public FlexBuilder ColumnReverse => ChainWithRule("direction", "column-reverse");

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

    // Align content properties
    /// <summary>
    /// Chain with align content start for the next rule.
    /// </summary>
    public FlexBuilder AlignContentStart => ChainWithRule("align-content", "start");

    /// <summary>
    /// Chain with align content end for the next rule.
    /// </summary>
    public FlexBuilder AlignContentEnd => ChainWithRule("align-content", "end");

    /// <summary>
    /// Chain with align content center for the next rule.
    /// </summary>
    public FlexBuilder AlignContentCenter => ChainWithRule("align-content", "center");

    /// <summary>
    /// Chain with align content between for the next rule.
    /// </summary>
    public FlexBuilder AlignContentBetween => ChainWithRule("align-content", "between");

    /// <summary>
    /// Chain with align content around for the next rule.
    /// </summary>
    public FlexBuilder AlignContentAround => ChainWithRule("align-content", "around");

    /// <summary>
    /// Chain with align content stretch for the next rule.
    /// </summary>
    public FlexBuilder AlignContentStretch => ChainWithRule("align-content", "stretch");

    // Align self properties
    /// <summary>
    /// Chain with align self auto for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfAuto => ChainWithRule("align-self", "auto");

    /// <summary>
    /// Chain with align self start for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfStart => ChainWithRule("align-self", "start");

    /// <summary>
    /// Chain with align self end for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfEnd => ChainWithRule("align-self", "end");

    /// <summary>
    /// Chain with align self center for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfCenter => ChainWithRule("align-self", "center");

    /// <summary>
    /// Chain with align self baseline for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfBaseline => ChainWithRule("align-self", "baseline");

    /// <summary>
    /// Chain with align self stretch for the next rule.
    /// </summary>
    public FlexBuilder AlignSelfStretch => ChainWithRule("align-self", "stretch");

    // Flex grow/shrink properties
    /// <summary>
    /// Chain with flex-fill for the next rule.
    /// </summary>
    public FlexBuilder Fill => ChainWithRule("fill", "");

    /// <summary>
    /// Chain with flex-grow-0 for the next rule.
    /// </summary>
    public FlexBuilder Grow0 => ChainWithRule("grow", "0");

    /// <summary>
    /// Chain with flex-grow-1 for the next rule.
    /// </summary>
    public FlexBuilder Grow1 => ChainWithRule("grow", "1");

    /// <summary>
    /// Chain with flex-shrink-0 for the next rule.
    /// </summary>
    public FlexBuilder Shrink0 => ChainWithRule("shrink", "0");

    /// <summary>
    /// Chain with flex-shrink-1 for the next rule.
    /// </summary>
    public FlexBuilder Shrink1 => ChainWithRule("shrink", "1");

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
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
            var rule = _rules[i];
            var cls = GetFlexClass(rule.Property, rule.Value);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
            var rule = _rules[i];
            var css = GetCssProperty(rule.Property, rule.Value);
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
                "row-reverse" => "flex-row-reverse",
                "column" => "flex-column",
                "column-reverse" => "flex-column-reverse",
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
            "align-content" => value switch
            {
                "start" => "align-content-start",
                "end" => "align-content-end",
                "center" => "align-content-center",
                "between" => "align-content-between",
                "around" => "align-content-around",
                "stretch" => "align-content-stretch",
                _ => string.Empty
            },
            "align-self" => value switch
            {
                "auto" => "align-self-auto",
                "start" => "align-self-start",
                "end" => "align-self-end",
                "center" => "align-self-center",
                "baseline" => "align-self-baseline",
                "stretch" => "align-self-stretch",
                _ => string.Empty
            },
            "fill" => "flex-fill",
            "grow" => value switch
            {
                "0" => "flex-grow-0",
                "1" => "flex-grow-1",
                _ => string.Empty
            },
            "shrink" => value switch
            {
                "0" => "flex-shrink-0",
                "1" => "flex-shrink-1",
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
                "row-reverse" => "flex-direction: row-reverse",
                "column" => "flex-direction: column",
                "column-reverse" => "flex-direction: column-reverse",
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
            "align-content" => value switch
            {
                "start" => "align-content: flex-start",
                "end" => "align-content: flex-end",
                "center" => "align-content: center",
                "between" => "align-content: space-between",
                "around" => "align-content: space-around",
                "stretch" => "align-content: stretch",
                _ => null
            },
            "align-self" => value switch
            {
                "auto" => "align-self: auto",
                "start" => "align-self: flex-start",
                "end" => "align-self: flex-end",
                "center" => "align-self: center",
                "baseline" => "align-self: baseline",
                "stretch" => "align-self: stretch",
                _ => null
            },
            "fill" => "flex: 1 1 auto",
            "grow" => value switch
            {
                "0" => "flex-grow: 0",
                "1" => "flex-grow: 1",
                _ => null
            },
            "shrink" => value switch
            {
                "0" => "flex-shrink: 0",
                "1" => "flex-shrink: 1",
                _ => null
            },
            _ => null
        };
    }

}

