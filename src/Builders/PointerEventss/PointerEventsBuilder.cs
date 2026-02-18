using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified pointer events builder with fluent API for chaining pointer events rules.
/// </summary>
public sealed class PointerEventsBuilder : ICssBuilder
{
    private readonly List<PointerEventsRule> _rules = new(4);

    // ----- Class constants -----
    private const string _classNone = "pe-none";
    private const string _classAuto = "pe-auto";

    // ----- CSS prefix & style constants (Intellenum *Value are const, so const-interp is fine) -----
    private const string _pointerEventsPrefix = "pointer-events: ";

    private const string _styleNone = $"{_pointerEventsPrefix}none";
    private const string _styleAuto = $"{_pointerEventsPrefix}auto";
    private const string _styleInherit = $"{_pointerEventsPrefix}{GlobalKeyword.InheritValue}";
    private const string _styleInitial = $"{_pointerEventsPrefix}{GlobalKeyword.InitialValue}";
    private const string _styleUnset = $"{_pointerEventsPrefix}{GlobalKeyword.UnsetValue}";
    private const string _styleRevert = $"{_pointerEventsPrefix}{GlobalKeyword.RevertValue}";
    private const string _styleRevertLayer = $"{_pointerEventsPrefix}{GlobalKeyword.RevertLayerValue}";

    internal PointerEventsBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new PointerEventsRule(value, breakpoint));
    }

    internal PointerEventsBuilder(List<PointerEventsRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the pointer events to none.
    /// </summary>
    public PointerEventsBuilder None => Chain(PointerEventsKeyword.None);

    /// <summary>
    /// Sets the pointer events to auto.
    /// </summary>
    public PointerEventsBuilder Auto => Chain(PointerEventsKeyword.Auto);

    /// <summary>
    /// Sets the pointer events to inherit.
    /// </summary>
    public PointerEventsBuilder Inherit => Chain(GlobalKeyword.InheritValue);

    /// <summary>
    /// Sets the pointer events to initial.
    /// </summary>
    public PointerEventsBuilder Initial => Chain(GlobalKeyword.InitialValue);

    /// <summary>
    /// Sets the pointer events to revert.
    /// </summary>
    public PointerEventsBuilder Revert => Chain(GlobalKeyword.RevertValue);

    /// <summary>
    /// Sets the pointer events to revert-layer.
    /// </summary>
    public PointerEventsBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);

    /// <summary>
    /// Sets the pointer events to unset.
    /// </summary>
    public PointerEventsBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the pointer events on phone breakpoint.
    /// </summary>
    public PointerEventsBuilder OnPhone => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the pointer events on tablet breakpoint.
    /// </summary>
    public PointerEventsBuilder OnTablet => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the pointer events on laptop breakpoint.
    /// </summary>
    public PointerEventsBuilder OnLaptop => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the pointer events on desktop breakpoint.
    /// </summary>
    public PointerEventsBuilder OnDesktop => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the pointer events on widescreen breakpoint.
    /// </summary>
    public PointerEventsBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);

    /// <summary>
    /// Applies the pointer events on ultrawide breakpoint.
    /// </summary>
    public PointerEventsBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder Chain(string value)
    {
        _rules.Add(new PointerEventsRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or seed with "auto" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new PointerEventsRule(PointerEventsKeyword.Auto, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        PointerEventsRule last = _rules[lastIdx];
        _rules[lastIdx] = new PointerEventsRule(last.Value, bp);
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
            PointerEventsRule rule = _rules[i];

            string baseClass = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _classNone,
                PointerEventsKeyword.AutoValue => _classAuto,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.InsertBreakpointType(baseClass, bp);

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
            PointerEventsRule rule = _rules[i];

            string css = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _styleNone,
                PointerEventsKeyword.AutoValue => _styleAuto,
                GlobalKeyword.InheritValue => _styleInherit,
                GlobalKeyword.InitialValue => _styleInitial,
                GlobalKeyword.UnsetValue => _styleUnset,
                GlobalKeyword.RevertValue => _styleRevert,
                GlobalKeyword.RevertLayerValue => _styleRevertLayer,
                _ => string.Empty
            };

            if (css.Length == 0)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}
