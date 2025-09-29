using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Builders.Abstract;
using Soenneker.Quark.Builders.Utils;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark.Builders.TextDecorations;

public sealed class TextDecorationBuilder : ICssBuilder
{
    private readonly List<TextDecorationRule> _rules = new(4);

    // ----- Class name constants (compile-time) -----
    private const string _classNone = "text-decoration-none";
    private const string _classUnderline = "text-decoration-underline";
    private const string _classLineThrough = "text-decoration-line-through";

    // ----- CSS value prefix (compile-time) -----
    private const string _textDecorationPrefix = "text-decoration-line: ";

    // ----- Style constants (compile-time const interpolation with Intellenum consts) -----
    private const string _styleNone = $"{_textDecorationPrefix}{TextDecorationLineKeyword.NoneValue}";
    private const string _styleUnderline = $"{_textDecorationPrefix}{TextDecorationLineKeyword.UnderlineValue}";
    private const string _styleLineThrough = $"{_textDecorationPrefix}{TextDecorationLineKeyword.LineThroughValue}";

    private const string _styleInherit = $"{_textDecorationPrefix}{GlobalKeyword.InheritValue}";
    private const string _styleInitial = $"{_textDecorationPrefix}{GlobalKeyword.InitialValue}";
    private const string _styleUnset = $"{_textDecorationPrefix}{GlobalKeyword.UnsetValue}";
    private const string _styleRevert = $"{_textDecorationPrefix}{GlobalKeyword.RevertValue}";
    private const string _styleRevertLayer = $"{_textDecorationPrefix}{GlobalKeyword.RevertLayerValue}";

    internal TextDecorationBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextDecorationRule(value, breakpoint));
    }

    internal TextDecorationBuilder(List<TextDecorationRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextDecorationBuilder None => Chain(TextDecorationLineKeyword.NoneValue);
    public TextDecorationBuilder Underline => Chain(TextDecorationLineKeyword.UnderlineValue);
    public TextDecorationBuilder LineThrough => Chain(TextDecorationLineKeyword.LineThroughValue);

    public TextDecorationBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextDecorationBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextDecorationBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextDecorationBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextDecorationBuilder Unset => Chain(GlobalKeyword.UnsetValue);

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

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with a default if empty).</summary>
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
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            TextDecorationRule rule = _rules[i];

            string css = rule.Value switch
            {
                TextDecorationLineKeyword.NoneValue => _styleNone,
                TextDecorationLineKeyword.UnderlineValue => _styleUnderline,
                TextDecorationLineKeyword.LineThroughValue => _styleLineThrough,
                GlobalKeyword.InheritValue => _styleInherit,
                GlobalKeyword.InitialValue => _styleInitial,
                GlobalKeyword.UnsetValue => _styleUnset,
                GlobalKeyword.RevertValue => _styleRevert,
                GlobalKeyword.RevertLayerValue => _styleRevertLayer,

                _ => string.Empty
            };

            if (css.Length == 0)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(css);
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
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            // length = prefix + "-" + bp + remainder
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                // prefix
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;

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
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}
