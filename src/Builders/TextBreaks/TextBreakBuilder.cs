using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified text break builder with fluent API for chaining text break rules.
/// </summary>
public sealed class TextBreakBuilder : ICssBuilder
{
    private readonly List<TextBreakRule> _rules = new(4);

    // Tailwind word-break utility (for Quark Suite / shadcn)
    private const string _classTextBreak = "break-words";
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

    /// <summary>
    /// Enables word breaking.
    /// </summary>
    public TextBreakBuilder Enable => Chain(true);
    /// <summary>
    /// Disables word breaking.
    /// </summary>
    public TextBreakBuilder Disable => Chain(false);

    /// <summary>
    /// Applies the text break on phone breakpoint.
    /// </summary>
    public TextBreakBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text break on tablet breakpoint.
    /// </summary>
    public TextBreakBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text break on laptop breakpoint.
    /// </summary>
    public TextBreakBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text break on desktop breakpoint.
    /// </summary>
    public TextBreakBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text break on widescreen breakpoint.
    /// </summary>
    public TextBreakBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the text break on ultrawide breakpoint.
    /// </summary>
    public TextBreakBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBreakBuilder Chain(bool enabled)
    {
        _rules.Add(new TextBreakRule(enabled, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or seed with <c>true</c> if empty).</summary>
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
            TextBreakRule rule = _rules[i];
            if (!rule.Enabled)
                continue;

            string baseClass = _classTextBreak;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

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

}

