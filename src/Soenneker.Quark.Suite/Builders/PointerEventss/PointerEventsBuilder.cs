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

    // ----- CSS prefix & style constants (EnumValue *Value are const, so const-interp is fine) -----
    private const string _pointerEventsPrefix = "pointer-events: ";

    private const string _styleNone = $"{_pointerEventsPrefix}none";
    private const string _styleAuto = $"{_pointerEventsPrefix}auto";
    private const string _styleInherit = $"{_pointerEventsPrefix}{GlobalKeyword.Inherit}";
    private const string _styleInitial = $"{_pointerEventsPrefix}{GlobalKeyword.Initial}";
    private const string _styleUnset = $"{_pointerEventsPrefix}{GlobalKeyword.Unset}";
    private const string _styleRevert = $"{_pointerEventsPrefix}{GlobalKeyword.Revert}";
    private const string _styleRevertLayer = $"{_pointerEventsPrefix}{GlobalKeyword.RevertLayer}";

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
    public PointerEventsBuilder Inherit => Chain(GlobalKeyword.Inherit);

    /// <summary>
    /// Sets the pointer events to initial.
    /// </summary>
    public PointerEventsBuilder Initial => Chain(GlobalKeyword.Initial);

    /// <summary>
    /// Sets the pointer events to revert.
    /// </summary>
    public PointerEventsBuilder Revert => Chain(GlobalKeyword.Revert);

    /// <summary>
    /// Sets the pointer events to revert-layer.
    /// </summary>
    public PointerEventsBuilder RevertLayer => Chain(GlobalKeyword.RevertLayer);

    /// <summary>
    /// Sets the pointer events to unset.
    /// </summary>
    public PointerEventsBuilder Unset => Chain(GlobalKeyword.Unset);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder Chain(string value)
    {
        _rules.Add(new PointerEventsRule(value, null));
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
            var rule = _rules[i];

            var baseClass = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _classNone,
                PointerEventsKeyword.AutoValue => _classAuto,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            var rule = _rules[i];

            var css = rule.Value switch
            {
                PointerEventsKeyword.NoneValue => _styleNone,
                PointerEventsKeyword.AutoValue => _styleAuto,
                GlobalKeyword.Inherit => _styleInherit,
                GlobalKeyword.Initial => _styleInitial,
                GlobalKeyword.Unset => _styleUnset,
                GlobalKeyword.Revert => _styleRevert,
                GlobalKeyword.RevertLayer => _styleRevertLayer,
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
}
