using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Builder for list variant utility classes with fluent API for chaining rules.
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

    /// <summary>
    /// Sets the list variant to unstyled.
    /// </summary>
    public ListVariantBuilder Unstyled => Chain(ListVariantType.Unstyled);
    /// <summary>
    /// Sets the list variant to inline.
    /// </summary>
    public ListVariantBuilder Inline => Chain(ListVariantType.Inline);
    /// <summary>
    /// Sets the list variant to inline-item.
    /// </summary>
    public ListVariantBuilder InlineItem => Chain(ListVariantType.InlineItem);

    /// <summary>
    /// Applies the list variant on phone breakpoint.
    /// </summary>
    public ListVariantBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the list variant on tablet breakpoint.
    /// </summary>
    public ListVariantBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the list variant on laptop breakpoint.
    /// </summary>
    public ListVariantBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the list variant on desktop breakpoint.
    /// </summary>
    public ListVariantBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the list variant on widescreen breakpoint.
    /// </summary>
    public ListVariantBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the list variant on ultrawide breakpoint.
    /// </summary>
    public ListVariantBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

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

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
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

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
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

    /// <summary>
    /// Returns the CSS class string representation of this list variant builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}


