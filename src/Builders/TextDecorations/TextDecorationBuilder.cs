using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class TextDecorationBuilder : ICssBuilder
{
    private readonly List<TextDecorationRule> _rules = new(4);
    private string? _style;
    private string? _color;
    private string? _thickness;

    // ----- Class name constants (compile-time) -----
    private const string _classNone = "text-decoration-none";
    private const string _classUnderline = "text-decoration-underline";
    private const string _classLineThrough = "text-decoration-line-through";

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
    public TextDecorationBuilder None => Chain(TextDecorationLineKeyword.NoneValue);
    public TextDecorationBuilder Underline => Chain(TextDecorationLineKeyword.UnderlineValue);
    public TextDecorationBuilder LineThrough => Chain(TextDecorationLineKeyword.LineThroughValue);
    public TextDecorationBuilder Overline => Chain(TextDecorationLineKeyword.OverlineValue);

    // Global keywords
    public TextDecorationBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextDecorationBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextDecorationBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextDecorationBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextDecorationBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    // Decoration styles
    public TextDecorationBuilder Solid => SetStyle(TextDecorationStyleKeyword.SolidValue);
    public TextDecorationBuilder Double => SetStyle(TextDecorationStyleKeyword.DoubleValue);
    public TextDecorationBuilder Dotted => SetStyle(TextDecorationStyleKeyword.DottedValue);
    public TextDecorationBuilder Dashed => SetStyle(TextDecorationStyleKeyword.DashedValue);
    public TextDecorationBuilder Wavy => SetStyle(TextDecorationStyleKeyword.WavyValue);

    // Color methods
    public TextDecorationBuilder Color(string color)
    {
        _color = color;
        return this;
    }

    public TextDecorationBuilder Primary => Color("var(--bs-primary)");
    public TextDecorationBuilder Secondary => Color("var(--bs-secondary)");
    public TextDecorationBuilder Success => Color("var(--bs-success)");
    public TextDecorationBuilder Danger => Color("var(--bs-danger)");
    public TextDecorationBuilder Warning => Color("var(--bs-warning)");
    public TextDecorationBuilder Info => Color("var(--bs-info)");
    public TextDecorationBuilder Light => Color("var(--bs-light)");
    public TextDecorationBuilder Dark => Color("var(--bs-dark)");

    // Thickness methods
    public TextDecorationBuilder Thickness(string thickness)
    {
        _thickness = thickness;
        return this;
    }

    public TextDecorationBuilder Thin => Thickness("1px");
    public TextDecorationBuilder Medium => Thickness("2px");
    public TextDecorationBuilder Thick => Thickness("3px");

    // Breakpoints
    public TextDecorationBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextDecorationBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextDecorationBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextDecorationBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextDecorationBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextDecorationBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

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

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with a default if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextDecorationRule(TextDecorationLineKeyword.NoneValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextDecorationRule(last.Value, bp);
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

            var baseClass = rule.Value switch
            {
                TextDecorationLineKeyword.NoneValue => _classNone,
                TextDecorationLineKeyword.UnderlineValue => _classUnderline,
                TextDecorationLineKeyword.LineThroughValue => _classLineThrough,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                baseClass = InsertBreakpointType(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

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
                var rule = _rules[^1]; // Use the last rule
                var lineValue = rule.Value switch
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

    /// <summary>
    /// Insert BreakpointType token as: "text-decoration-underline" + "md" ? "text-decoration-md-underline".
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
