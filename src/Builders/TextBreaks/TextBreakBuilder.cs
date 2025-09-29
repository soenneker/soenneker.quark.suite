using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class TextBreakBuilder : ICssBuilder
{
    private readonly List<TextBreakRule> _rules = new(4);

    // ----- Class/Style constants -----
    private const string _classTextBreak = "text-break";
    private const string _styleBreakWord = "word-wrap: break-word"; // keep alias to match your original

    internal TextBreakBuilder(bool enabled, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextBreakRule(enabled, breakpoint));
    }

    internal TextBreakBuilder(List<TextBreakRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextBreakBuilder Enable => Chain(true);
    public TextBreakBuilder Disable => Chain(false);

    public TextBreakBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextBreakBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextBreakBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextBreakBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextBreakBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextBreakBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBreakBuilder Chain(bool enabled)
    {
        _rules.Add(new TextBreakRule(enabled, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with <c>true</c> if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBreakBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextBreakRule(true, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextBreakRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextBreakRule(last.Enabled, bp);
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
            TextBreakRule rule = _rules[i];
            if (!rule.Enabled)
                continue;

            string baseClass = _classTextBreak;

            string bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                baseClass = InsertBreakpointType(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

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
            if (!_rules[i].Enabled)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_styleBreakWord);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

    /// <summary>
    /// Insert BreakpointType token as: "text-break" + "md" ? "text-md-break".
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

