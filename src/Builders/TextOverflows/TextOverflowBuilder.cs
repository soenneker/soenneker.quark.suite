using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

using TextOverflowEnum = Soenneker.Quark.Enums.TextOverflowKeyword;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text-overflow builder with fluent API for chaining rules.
/// </summary>
public sealed class TextOverflowBuilder : ICssBuilder
{
    private readonly List<TextOverflowRule> _rules = new(4);

    // ----- Class constants -----
    private const string _classTruncate = "text-truncate";

    // ----- CSS prefix -----
    private const string _textOverflowPrefix = "text-overflow: ";

    internal TextOverflowBuilder(TextOverflowEnum textOverflow, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOverflowRule(textOverflow.Value, breakpoint));
    }

    internal TextOverflowBuilder(string textOverflow, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOverflowRule(textOverflow, breakpoint));
    }

    internal TextOverflowBuilder(List<TextOverflowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Fluent chaining (TextOverflow enum) -----
    public TextOverflowBuilder Clip => Chain(TextOverflowEnum.Clip);
    public TextOverflowBuilder Ellipsis => Chain(TextOverflowEnum.Ellipsis);

    // ----- Fluent chaining (Global keywords) -----
    public TextOverflowBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public TextOverflowBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public TextOverflowBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public TextOverflowBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public TextOverflowBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    // ----- BreakpointType chaining -----
    public TextOverflowBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextOverflowBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextOverflowBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextOverflowBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextOverflowBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextOverflowBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(TextOverflowEnum value)
    {
        _rules.Add(new TextOverflowRule(value.Value, null));
        return this;
    }

    // Overload for global keywords (assumes a ctor: TextOverflowRule(GlobalKeyword, BreakpointType?))
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(GlobalKeyword keyword)
    {
        _rules.Add(new TextOverflowRule(keyword.Value, null));
        return this;
    }

    // Overload for string values
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(string value)
    {
        _rules.Add(new TextOverflowRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with Clip if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextOverflowRule(TextOverflowEnum.Clip, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextOverflowRule last = _rules[lastIdx];

        // Re-create the rule preserving its text-overflow value using the string value.
        _rules[lastIdx] = new TextOverflowRule(last.Value, bp);
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
            TextOverflowRule rule = _rules[i];

            // Only Clip/Ellipsis map to a Bootstrap class; keywords don't.
            string baseClass = GetTextOverflowClass(rule.Value);
            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                baseClass = InsertBreakpointType(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(baseClass);
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
            TextOverflowRule rule = _rules[i];

            // Always use the string value (covers Clip/Ellipsis and any keyword values)
            string value = rule.Value;

            if (string.IsNullOrEmpty(value))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_textOverflowPrefix);
            sb.Append(value);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetTextOverflowClass(string textOverflow)
    {
        switch (textOverflow)
        {
            case "clip":
            case "ellipsis":
                return _classTruncate;
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Insert BreakpointType token as: "text-truncate" + "md" → "text-md-truncate".
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
