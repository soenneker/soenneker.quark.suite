using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified screen reader builder with fluent API for chaining screen reader rules.
/// </summary>
public sealed class ScreenReaderBuilder : ICssBuilder
{
    private readonly List<ScreenReaderRule> _rules = new(4);

    private const string _classSrOnly = "sr-only";
    private const string _classSrOnlyFocusable = "sr-only-focusable";

    internal ScreenReaderBuilder(string type, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScreenReaderRule(type, breakpoint));
    }

    internal ScreenReaderBuilder(List<ScreenReaderRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the screen reader to only (sr-only).
    /// </summary>
    public ScreenReaderBuilder Only => ChainWithType("only");
    /// <summary>
    /// Sets the screen reader to only-focusable (sr-only-focusable).
    /// </summary>
    public ScreenReaderBuilder OnlyFocusable => ChainWithType("only-focusable");

    /// <summary>
    /// Applies the screen reader on phone breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the screen reader on tablet breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the screen reader on laptop breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the screen reader on desktop breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the screen reader on widescreen breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the screen reader on ultrawide breakpoint.
    /// </summary>
    public ScreenReaderBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScreenReaderBuilder ChainWithType(string type)
    {
        _rules.Add(new ScreenReaderRule(type, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScreenReaderBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ScreenReaderRule("only", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ScreenReaderRule last = _rules[lastIdx];
        _rules[lastIdx] = new ScreenReaderRule(last.Type, breakpoint);
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
            ScreenReaderRule rule = _rules[i];
            string cls = GetScreenReaderClass(rule.Type);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
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
            ScreenReaderRule rule = _rules[i];
            string? styleValue = GetScreenReaderStyle(rule.Type);

            if (styleValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(styleValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetScreenReaderClass(string type)
    {
        return type switch
        {
            "only" => _classSrOnly,
            "only-focusable" => _classSrOnlyFocusable,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetScreenReaderStyle(string type)
    {
        return type switch
        {
            "only" => "position: absolute; width: 1px; height: 1px; padding: 0; margin: -1px; overflow: hidden; clip: rect(0, 0, 0, 0); white-space: nowrap; border: 0",
            "only-focusable" => "position: absolute; width: 1px; height: 1px; padding: 0; margin: -1px; overflow: hidden; clip: rect(0, 0, 0, 0); white-space: nowrap; border: 0; &:focus { position: static; width: auto; height: auto; padding: inherit; margin: inherit; overflow: visible; clip: auto; white-space: normal; }",
            _ => null
        };
    }

    /// <summary>
    /// Returns the CSS class string representation of this screen reader builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

