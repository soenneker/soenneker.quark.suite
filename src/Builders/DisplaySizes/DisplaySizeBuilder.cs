using Soenneker.Quark.Enums;
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

    /// <summary>
    /// Gets a value indicating whether the builder is empty (has no rules).
    /// </summary>
    public bool IsEmpty => _rules.Count == 0;

    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes.
    /// </summary>
    public bool IsCssClass => true;

    /// <summary>
    /// Gets a value indicating whether this builder generates CSS styles.
    /// </summary>
    public bool IsCssStyle => false;

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
    /// <summary>
    /// Sets the display size to 1.
    /// </summary>
    public DisplaySizeBuilder Is1 => ChainSize("1");

    /// <summary>
    /// Sets the display size to 2.
    /// </summary>
    public DisplaySizeBuilder Is2 => ChainSize("2");

    /// <summary>
    /// Sets the display size to 3.
    /// </summary>
    public DisplaySizeBuilder Is3 => ChainSize("3");

    /// <summary>
    /// Sets the display size to 4.
    /// </summary>
    public DisplaySizeBuilder Is4 => ChainSize("4");

    /// <summary>
    /// Sets the display size to 5.
    /// </summary>
    public DisplaySizeBuilder Is5 => ChainSize("5");

    /// <summary>
    /// Sets the display size to 6.
    /// </summary>
    public DisplaySizeBuilder Is6 => ChainSize("6");

    // ----- BreakpointType chaining -----
    /// <summary>
    /// Applies the display size on phone breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnPhone => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the display size on tablet breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnTablet => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the display size on laptop breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnLaptop => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the display size on desktop breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnDesktop => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the display size on widescreen breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);

    /// <summary>
    /// Applies the display size on ultrawide breakpoint.
    /// </summary>
    public DisplaySizeBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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

        int lastIdx = _rules.Count - 1;
        DisplaySizeRule last = _rules[lastIdx];
        _rules[lastIdx] = new DisplaySizeRule(last.Size, bp);
        return this;
    }

    /// <summary>
    /// Returns the CSS class string representation of this display size builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString() => ToClass();

    /// <summary>Gets the CSS class string for the current configuration.</summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            DisplaySizeRule rule = _rules[i];

            string sizeClass = GetSizeClass(rule.Size);
            if (sizeClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                sizeClass = BreakpointUtil.InsertBreakpointType(sizeClass, bp);

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
            DisplaySizeRule rule = _rules[i];

            string? sizeValue = GetSizeValue(rule.Size);
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
}