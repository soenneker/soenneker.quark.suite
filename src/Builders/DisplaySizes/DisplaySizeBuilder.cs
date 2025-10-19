using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance display size builder with fluent API for chaining display heading size rules.
/// </summary>
public sealed class DisplaySizeBuilder : ICssBuilder
{
    private readonly List<DisplaySizeRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _display1 = "display-1";
    private const string _display2 = "display-2";
    private const string _display3 = "display-3";
    private const string _display4 = "display-4";
    private const string _display5 = "display-5";
    private const string _display6 = "display-6";

    // ----- CSS prefix (compile-time) -----
    private const string _fontSizePrefix = "font-size: ";

    internal DisplaySizeBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new DisplaySizeRule(size, breakpoint));
    }

    internal DisplaySizeBuilder(List<DisplaySizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Fluent size chaining -----
    public DisplaySizeBuilder Is1 => ChainSize("1");
    public DisplaySizeBuilder Is2 => ChainSize("2");
    public DisplaySizeBuilder Is3 => ChainSize("3");
    public DisplaySizeBuilder Is4 => ChainSize("4");
    public DisplaySizeBuilder Is5 => ChainSize("5");
    public DisplaySizeBuilder Is6 => ChainSize("6");

    // ----- BreakpointType chaining -----
    public DisplaySizeBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public DisplaySizeBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public DisplaySizeBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public DisplaySizeBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public DisplaySizeBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public DisplaySizeBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplaySizeBuilder ChainSize(string size)
    {
        _rules.Add(new DisplaySizeRule(size, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "4" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplaySizeBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new DisplaySizeRule("4", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new DisplaySizeRule(last.Size, bp);
        return this;
    }

    /// <summary>Gets the CSS class string for the current configuration.</summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];

            var sizeClass = GetSizeClass(rule.Size);
            if (sizeClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                sizeClass = InsertBreakpointType(sizeClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(sizeClass);
        }

        return sb.ToString();
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];

            var sizeValue = GetSizeValue(rule.Size);
            if (sizeValue is null)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(_fontSizePrefix);
            sb.Append(sizeValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeClass(string size)
    {
        // Bootstrap 5 display heading size mapping
        return size switch
        {
            "1" => _display1,
            "2" => _display2,
            "3" => _display3,
            "4" => _display4,
            "5" => _display5,
            "6" => _display6,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        // Use Bootstrap CSS variables for inline styles
        return size switch
        {
            "1" => "calc(1.625rem + 4.5vw)",
            "2" => "calc(1.575rem + 3.9vw)",
            "3" => "calc(1.525rem + 3.3vw)",
            "4" => "calc(1.475rem + 2.7vw)",
            "5" => "calc(1.425rem + 2.1vw)",
            "6" => "calc(1.375rem + 1.5vw)",
            _ => size
        };
    }


    /// <summary>
    /// Insert BreakpointType token as: "display-4" + "md" → "display-md-4".
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

