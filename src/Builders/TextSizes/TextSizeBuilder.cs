using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text size builder with fluent API for chaining text size rules.
/// </summary>
public sealed class TextSizeBuilder : ICssBuilder
{
    private readonly List<TextSizeRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _fs6 = "fs-6";
    private const string _fs5 = "fs-5";
    private const string _fs4 = "fs-4";
    private const string _fs3 = "fs-3";
    private const string _fs2 = "fs-2";

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
    public TextSizeBuilder ExtraSmall => ChainSize(SizeType.ExtraSmall.Value);
    public TextSizeBuilder Small => ChainSize(SizeType.Small.Value);
    public TextSizeBuilder Base => ChainSize("base");
    public TextSizeBuilder Large => ChainSize(SizeType.Large.Value);
    public TextSizeBuilder ExtraLarge => ChainSize(SizeType.ExtraLarge.Value);

    // ----- BreakpointType chaining -----
    public TextSizeBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextSizeBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextSizeBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextSizeBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextSizeBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextSizeBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder ChainSize(string size)
    {
        _rules.Add(new TextSizeRule(size, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "base" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextSizeRule("base", bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextSizeRule last = _rules[lastIdx];
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
            TextSizeRule rule = _rules[i];

            string sizeClass = GetSizeClass(rule.Size);
            if (sizeClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
            TextSizeRule rule = _rules[i];

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
        // Bootstrap 5 font-scale mapping
        return size switch
        {
            "xs" => _fs6,
            "sm" => _fs5,
            "base" => _fs4,
            "lg" => _fs3,
            "xl" => _fs2,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        // Tailored inline values for when you need styles instead of classes
        return size switch
        {
            "xs" => "0.75rem",
            "sm" => "0.875rem",
            "base" => "1rem",
            "lg" => "1.125rem",
            "xl" => "1.25rem",
            _ => null
        };
    }


    /// <summary>
    /// Insert BreakpointType token as: "fs-4" + "md" → "fs-md-4".
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
