using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    /// <summary>
    /// Sets the resize to none.
    /// </summary>
    public ResizeBuilder None => ChainWithResize("none");
    /// <summary>
    /// Sets the resize to both.
    /// </summary>
    public ResizeBuilder Both => ChainWithResize("both");
    /// <summary>
    /// Sets the resize to horizontal.
    /// </summary>
    public ResizeBuilder Horizontal => ChainWithResize("horizontal");
    /// <summary>
    /// Sets the resize to vertical.
    /// </summary>
    public ResizeBuilder Vertical => ChainWithResize("vertical");

    /// <summary>
    /// Applies the resize on phone breakpoint.
    /// </summary>
    public ResizeBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the resize on tablet breakpoint.
    /// </summary>
    public ResizeBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the resize on laptop breakpoint.
    /// </summary>
    public ResizeBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the resize on desktop breakpoint.
    /// </summary>
    public ResizeBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the resize on widescreen breakpoint.
    /// </summary>
    public ResizeBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the resize on ultrawide breakpoint.
    /// </summary>
    public ResizeBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

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
            ResizeRule rule = _rules[i];
            string cls = GetResizeClass(rule.Resize);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
    /// <returns>The CSS style string.</returns>
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

    /// <summary>
    /// Returns the CSS class string representation of this resize builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

