using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class TextAlignmentBuilder : ICssBuilder
{
    private readonly List<TextAlignmentRule> _rules = new(4);

    // ----- Class name constants (compile-time) -----
    private const string _classStart = "text-start";
    private const string _classCenter = "text-center";
    private const string _classEnd = "text-end";

    // ----- CSS prefix (compile-time) -----
    private const string _textAlignPrefix = "text-align: ";

    // ----- Style constants (compile-time, Intellenum *Value are const) -----
    private const string _styleStart = $"{_textAlignPrefix}{TextAlignKeyword.StartValue}";
    private const string _styleCenter = $"{_textAlignPrefix}{TextAlignKeyword.CenterValue}";
    private const string _styleEnd = $"{_textAlignPrefix}{TextAlignKeyword.EndValue}";
    private const string _styleInherit = $"{_textAlignPrefix}{GlobalKeyword.InheritValue}";
    private const string _styleInitial = $"{_textAlignPrefix}{GlobalKeyword.InitialValue}";
    private const string _styleUnset = $"{_textAlignPrefix}{GlobalKeyword.UnsetValue}";
    private const string _styleRevert = $"{_textAlignPrefix}{GlobalKeyword.RevertValue}";
    private const string _styleRevertLayer = $"{_textAlignPrefix}{GlobalKeyword.RevertLayerValue}";

    internal TextAlignmentBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextAlignmentRule(value, breakpoint));
    }

    internal TextAlignmentBuilder(List<TextAlignmentRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextAlignmentBuilder Start => Chain(TextAlignKeyword.StartValue);
    public TextAlignmentBuilder Center => Chain(TextAlignKeyword.CenterValue);
    public TextAlignmentBuilder End => Chain(TextAlignKeyword.EndValue);

    public TextAlignmentBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextAlignmentBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextAlignmentBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextAlignmentBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextAlignmentBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public TextAlignmentBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextAlignmentBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextAlignmentBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextAlignmentBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextAlignmentBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextAlignmentBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextAlignmentBuilder Chain(string value)
    {
        _rules.Add(new TextAlignmentRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with a default if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextAlignmentBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextAlignmentRule(TextAlignKeyword.StartValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextAlignmentRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextAlignmentRule(last.Value, bp);
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
            TextAlignmentRule rule = _rules[i];

            string baseClass = rule.Value switch
            {
                TextAlignKeyword.StartValue => _classStart,
                TextAlignKeyword.CenterValue => _classCenter,
                TextAlignKeyword.EndValue => _classEnd,
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
            TextAlignmentRule rule = _rules[i];

            string css = rule.Value switch
            {
                TextAlignKeyword.StartValue => _styleStart,
                TextAlignKeyword.CenterValue => _styleCenter,
                TextAlignKeyword.EndValue => _styleEnd,

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
    private static string GetBp(BreakpointType? breakpoint)
    {
        return breakpoint?.Value ?? string.Empty;
    }

    /// <summary>
    /// Insert BreakpointType token as: "text-center" + "md" ? "text-md-center".
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
