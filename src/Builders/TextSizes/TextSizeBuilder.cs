using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text size builder with fluent API for chaining text size rules.
/// </summary>
public sealed class TextSizeBuilder : ICssBuilder
{
    private readonly List<TextSizeRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _fs1 = "fs-1";
    private const string _fs2 = "fs-2";
    private const string _fs3 = "fs-3";
    private const string _fs4 = "fs-4";
    private const string _fs5 = "fs-5";
    private const string _fs6 = "fs-6";

    // ----- CSS prefix (compile-time) -----
    private const string _fontSizePrefix = "font-size: ";

    internal TextSizeBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextSizeRule(size, breakpoint));
    }

    internal TextSizeBuilder(List<TextSizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Fluent size chaining -----
    /// <summary>
    /// Sets the text size to 1.
    /// </summary>
    public TextSizeBuilder Is1 => ChainSize("1");
    /// <summary>
    /// Sets the text size to 2.
    /// </summary>
    public TextSizeBuilder Is2 => ChainSize("2");
    /// <summary>
    /// Sets the text size to 3.
    /// </summary>
    public TextSizeBuilder Is3 => ChainSize("3");
    /// <summary>
    /// Sets the text size to 4.
    /// </summary>
    public TextSizeBuilder Is4 => ChainSize("4");
    /// <summary>
    /// Sets the text size to 5.
    /// </summary>
    public TextSizeBuilder Is5 => ChainSize("5");
    /// <summary>
    /// Sets the text size to 6.
    /// </summary>
    public TextSizeBuilder Is6 => ChainSize("6");

    // ----- BreakpointType chaining -----
    /// <summary>
    /// Applies the text size on phone breakpoint.
    /// </summary>
    public TextSizeBuilder OnPhone => ChainBp(BreakpointType.Phone);
    /// <summary>
    /// Applies the text size on tablet breakpoint.
    /// </summary>
    public TextSizeBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    /// <summary>
    /// Applies the text size on laptop breakpoint.
    /// </summary>
    public TextSizeBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    /// <summary>
    /// Applies the text size on desktop breakpoint.
    /// </summary>
    public TextSizeBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    /// <summary>
    /// Applies the text size on widescreen breakpoint.
    /// </summary>
    public TextSizeBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the text size on ultrawide breakpoint.
    /// </summary>
    public TextSizeBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder ChainSize(string size)
    {
        _rules.Add(new TextSizeRule(size, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "4" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextSizeRule("4", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextSizeRule(last.Size, bp);
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

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
        // Bootstrap 5 font-size mapping
        return size switch
        {
            "1" => _fs1,
            "2" => _fs2,
            "3" => _fs3,
            "4" => _fs4,
            "5" => _fs5,
            "6" => _fs6,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        // Use Bootstrap CSS variables for inline styles
        return size switch
        {
            "1" => "calc(1.375rem + 1.5vw)",
            "2" => "calc(1.325rem + 0.9vw)",
            "3" => "calc(1.3rem + 0.6vw)",
            "4" => "calc(1.275rem + 0.3vw)",
            "5" => "1.25rem",
            "6" => "1rem",
            _ => size
        };
    }


}
