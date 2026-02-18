using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text decoration builder with fluent API for chaining text decoration rules.
/// </summary>
public sealed class TextDecorationBuilder : ICssBuilder
{
    private readonly List<TextDecorationRule> _rules = new(4);
    private string? _style;
    private string? _color;
    private string? _thickness;

    // Tailwind text-decoration utilities (for Quark Suite / shadcn)
    private const string _classNone = "no-underline";
    private const string _classUnderline = "underline";
    private const string _classLineThrough = "line-through";
    private const string _classOverline = "overline";

    internal TextDecorationBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextDecorationRule(value, breakpoint));
    }

    internal TextDecorationBuilder(List<TextDecorationRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Line types
    /// <summary>
    /// Sets the text decoration to none (no decoration).
    /// </summary>
    public TextDecorationBuilder None => Chain(TextDecorationLineKeyword.NoneValue);
    /// <summary>
    /// Sets the text decoration to underline.
    /// </summary>
    public TextDecorationBuilder Underline => Chain(TextDecorationLineKeyword.UnderlineValue);
    /// <summary>
    /// Sets the text decoration to line-through.
    /// </summary>
    public TextDecorationBuilder LineThrough => Chain(TextDecorationLineKeyword.LineThroughValue);
    /// <summary>
    /// Sets the text decoration to overline.
    /// </summary>
    public TextDecorationBuilder Overline => Chain(TextDecorationLineKeyword.OverlineValue);

    // Global keywords
    /// <summary>
    /// Sets the text decoration to inherit.
    /// </summary>
    public TextDecorationBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the text decoration to initial.
    /// </summary>
    public TextDecorationBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the text decoration to revert.
    /// </summary>
    public TextDecorationBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the text decoration to revert-layer.
    /// </summary>
    public TextDecorationBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the text decoration to unset.
    /// </summary>
    public TextDecorationBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    // Decoration styles
    /// <summary>
    /// Sets the text decoration style to solid.
    /// </summary>
    public TextDecorationBuilder Solid => SetStyle(TextDecorationStyleKeyword.SolidValue);
    /// <summary>
    /// Sets the text decoration style to double.
    /// </summary>
    public TextDecorationBuilder Double => SetStyle(TextDecorationStyleKeyword.DoubleValue);
    /// <summary>
    /// Sets the text decoration style to dotted.
    /// </summary>
    public TextDecorationBuilder Dotted => SetStyle(TextDecorationStyleKeyword.DottedValue);
    /// <summary>
    /// Sets the text decoration style to dashed.
    /// </summary>
    public TextDecorationBuilder Dashed => SetStyle(TextDecorationStyleKeyword.DashedValue);
    /// <summary>
    /// Sets the text decoration style to wavy.
    /// </summary>
    public TextDecorationBuilder Wavy => SetStyle(TextDecorationStyleKeyword.WavyValue);

    // Color methods
    /// <summary>
    /// Sets the text decoration color to the specified value.
    /// </summary>
    /// <param name="color">The color value (e.g., "#ff0000", "red", "var(--primary)").</param>
    /// <returns>This builder instance for method chaining.</returns>
    public TextDecorationBuilder Color(string color)
    {
        _color = color;
        return this;
    }

    /// <summary>
    /// Sets the text decoration color to primary theme color.
    /// </summary>
    public TextDecorationBuilder Primary => Color("var(--primary)");
    /// <summary>
    /// Sets the text decoration color to secondary theme color.
    /// </summary>
    public TextDecorationBuilder Secondary => Color("var(--secondary)");
    /// <summary>
    /// Sets the text decoration color to success theme color.
    /// </summary>
    public TextDecorationBuilder Success => Color("var(--success)");
    /// <summary>
    /// Sets the text decoration color to danger theme color.
    /// </summary>
    public TextDecorationBuilder Danger => Color("var(--danger)");
    /// <summary>
    /// Sets the text decoration color to warning theme color.
    /// </summary>
    public TextDecorationBuilder Warning => Color("var(--warning)");
    /// <summary>
    /// Sets the text decoration color to info theme color.
    /// </summary>
    public TextDecorationBuilder Info => Color("var(--info)");
    /// <summary>
    /// Sets the text decoration color to light theme color.
    /// </summary>
    public TextDecorationBuilder Light => Color("var(--light)");
    /// <summary>
    /// Sets the text decoration color to dark theme color.
    /// </summary>
    public TextDecorationBuilder Dark => Color("var(--dark)");

    // Thickness methods
    /// <summary>
    /// Sets the text decoration thickness to the specified value.
    /// </summary>
    /// <param name="thickness">The thickness value (e.g., "1px", "2px", "3px").</param>
    /// <returns>This builder instance for method chaining.</returns>
    public TextDecorationBuilder Thickness(string thickness)
    {
        _thickness = thickness;
        return this;
    }

    /// <summary>
    /// Sets the text decoration thickness to thin (1px).
    /// </summary>
    public TextDecorationBuilder Thin => Thickness("1px");
    /// <summary>
    /// Sets the text decoration thickness to medium (2px).
    /// </summary>
    public TextDecorationBuilder Medium => Thickness("2px");
    /// <summary>
    /// Sets the text decoration thickness to thick (3px).
    /// </summary>
    public TextDecorationBuilder Thick => Thickness("3px");

    // Breakpoints
    /// <summary>
    /// Applies the text decoration on phone breakpoint.
    /// </summary>
    public TextDecorationBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text decoration on tablet breakpoint.
    /// </summary>
    public TextDecorationBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text decoration on laptop breakpoint.
    /// </summary>
    public TextDecorationBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text decoration on desktop breakpoint.
    /// </summary>
    public TextDecorationBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text decoration on widescreen breakpoint.
    /// </summary>
    public TextDecorationBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the text decoration on ultrawide breakpoint.
    /// </summary>
    public TextDecorationBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder Chain(string value)
    {
        _rules.Add(new TextDecorationRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder SetStyle(string style)
    {
        _style = style;
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or seed with a default if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextDecorationRule(TextDecorationLineKeyword.NoneValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextDecorationRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextDecorationRule(last.Value, bp);
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
            TextDecorationRule rule = _rules[i];

            string baseClass = rule.Value switch
            {
                TextDecorationLineKeyword.NoneValue => _classNone,
                TextDecorationLineKeyword.UnderlineValue => _classUnderline,
                TextDecorationLineKeyword.LineThroughValue => _classLineThrough,
                TextDecorationLineKeyword.OverlineValue => _classOverline,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        if (_rules.Count == 0 && _style == null && _color == null && _thickness == null)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        
        // Build the text-decoration shorthand or individual properties
        if (_rules.Count > 0 || _style != null || _color != null || _thickness != null)
        {
            sb.Append("text-decoration:");
            
            // Line (underline, line-through, etc.)
            if (_rules.Count > 0)
            {
                TextDecorationRule rule = _rules[^1]; // Use the last rule
                string lineValue = rule.Value switch
                {
                    TextDecorationLineKeyword.NoneValue => "none",
                    TextDecorationLineKeyword.UnderlineValue => "underline",
                    TextDecorationLineKeyword.LineThroughValue => "line-through",
                    TextDecorationLineKeyword.OverlineValue => "overline",
                    GlobalKeyword.InheritValue => "inherit",
                    GlobalKeyword.InitialValue => "initial",
                    GlobalKeyword.UnsetValue => "unset",
                    GlobalKeyword.RevertValue => "revert",
                    GlobalKeyword.RevertLayerValue => "revert-layer",
                    _ => "none"
                };
                sb.Append(' ');
                sb.Append(lineValue);
            }
            else
            {
                sb.Append(" underline"); // Default if only style/color/thickness specified
            }
            
            // Style (solid, dotted, wavy, etc.)
            if (_style != null)
            {
                sb.Append(' ');
                sb.Append(_style);
            }
            
            // Color
            if (_color != null)
            {
                sb.Append(' ');
                sb.Append(_color);
            }
            
            // Thickness
            if (_thickness != null)
            {
                sb.Append(' ');
                sb.Append(_thickness);
            }
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}
