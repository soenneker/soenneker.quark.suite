using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance link underline builder with fluent API for chaining link underline rules.
/// </summary>
public sealed class LinkUnderlineBuilder : ICssBuilder
{
    private readonly List<LinkUnderlineRule> _rules = new(4);

    internal LinkUnderlineBuilder(string value, string type, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LinkUnderlineRule(value, type, breakpoint));
    }

    internal LinkUnderlineBuilder(List<LinkUnderlineRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Opacity methods
    /// <summary>
    /// Sets the link underline opacity to 0.
    /// </summary>
    public LinkUnderlineBuilder Opacity0 => ChainOpacity("0");
    /// <summary>
    /// Sets the link underline opacity to 10.
    /// </summary>
    public LinkUnderlineBuilder Opacity10 => ChainOpacity("10");
    /// <summary>
    /// Sets the link underline opacity to 25.
    /// </summary>
    public LinkUnderlineBuilder Opacity25 => ChainOpacity("25");
    /// <summary>
    /// Sets the link underline opacity to 50.
    /// </summary>
    public LinkUnderlineBuilder Opacity50 => ChainOpacity("50");
    /// <summary>
    /// Sets the link underline opacity to 75.
    /// </summary>
    public LinkUnderlineBuilder Opacity75 => ChainOpacity("75");
    /// <summary>
    /// Sets the link underline opacity to 100.
    /// </summary>
    public LinkUnderlineBuilder Opacity100 => ChainOpacity("100");

    // Color methods
    /// <summary>
    /// Sets the link underline color to primary.
    /// </summary>
    public LinkUnderlineBuilder Primary => ChainColor("primary");
    /// <summary>
    /// Sets the link underline color to secondary.
    /// </summary>
    public LinkUnderlineBuilder Secondary => ChainColor("secondary");
    /// <summary>
    /// Sets the link underline color to success.
    /// </summary>
    public LinkUnderlineBuilder Success => ChainColor("success");
    /// <summary>
    /// Sets the link underline color to info.
    /// </summary>
    public LinkUnderlineBuilder Info => ChainColor("info");
    /// <summary>
    /// Sets the link underline color to warning.
    /// </summary>
    public LinkUnderlineBuilder Warning => ChainColor("warning");
    /// <summary>
    /// Sets the link underline color to danger.
    /// </summary>
    public LinkUnderlineBuilder Danger => ChainColor("danger");
    /// <summary>
    /// Sets the link underline color to light.
    /// </summary>
    public LinkUnderlineBuilder Light => ChainColor("light");
    /// <summary>
    /// Sets the link underline color to dark.
    /// </summary>
    public LinkUnderlineBuilder Dark => ChainColor("dark");

    /// <summary>
    /// Applies the link underline on phone breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnPhone => ChainBp(BreakpointType.Phone);
    /// <summary>
    /// Applies the link underline on tablet breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    /// <summary>
    /// Applies the link underline on laptop breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    /// <summary>
    /// Applies the link underline on desktop breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    /// <summary>
    /// Applies the link underline on widescreen breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the link underline on ultrawide breakpoint.
    /// </summary>
    public LinkUnderlineBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkUnderlineBuilder ChainOpacity(string value)
    {
        _rules.Add(new LinkUnderlineRule(value, "opacity", null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkUnderlineBuilder ChainColor(string value)
    {
        _rules.Add(new LinkUnderlineRule(value, "color", null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkUnderlineBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LinkUnderlineRule("", "base", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LinkUnderlineRule(last.Value, last.Type, bp);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            string cls;

            if (rule.Type == "opacity")
            {
                cls = GetOpacityClass(rule.Value);
            }
            else if (rule.Type == "color")
            {
                cls = GetColorClass(rule.Value);
            }
            else
            {
                cls = "link-underline";
            }

            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            string? css = null;

            if (rule.Type == "opacity")
            {
                css = GetOpacityStyle(rule.Value);
            }
            else if (rule.Type == "color")
            {
                css = GetColorStyle(rule.Value);
            }
            else
            {
                css = "text-decoration-line: underline";
            }

            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetOpacityClass(string value)
    {
        return value switch
        {
            "0" => "link-underline-opacity-0",
            "10" => "link-underline-opacity-10",
            "25" => "link-underline-opacity-25",
            "50" => "link-underline-opacity-50",
            "75" => "link-underline-opacity-75",
            "100" => "link-underline-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColorClass(string value)
    {
        return value switch
        {
            "primary" => "link-underline-primary",
            "secondary" => "link-underline-secondary",
            "success" => "link-underline-success",
            "info" => "link-underline-info",
            "warning" => "link-underline-warning",
            "danger" => "link-underline-danger",
            "light" => "link-underline-light",
            "dark" => "link-underline-dark",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetOpacityStyle(string value)
    {
        return value switch
        {
            "0" => "--bs-link-underline-opacity: 0",
            "10" => "--bs-link-underline-opacity: 0.1",
            "25" => "--bs-link-underline-opacity: 0.25",
            "50" => "--bs-link-underline-opacity: 0.5",
            "75" => "--bs-link-underline-opacity: 0.75",
            "100" => "--bs-link-underline-opacity: 1",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetColorStyle(string value)
    {
        return value switch
        {
            "primary" => "--bs-link-underline: var(--bs-primary)",
            "secondary" => "--bs-link-underline: var(--bs-secondary)",
            "success" => "--bs-link-underline: var(--bs-success)",
            "info" => "--bs-link-underline: var(--bs-info)",
            "warning" => "--bs-link-underline: var(--bs-warning)",
            "danger" => "--bs-link-underline: var(--bs-danger)",
            "light" => "--bs-link-underline: var(--bs-light)",
            "dark" => "--bs-link-underline: var(--bs-dark)",
            _ => null
        };
    }

}







