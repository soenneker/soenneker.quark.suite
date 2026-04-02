using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Quark.Attributes;


namespace Soenneker.Quark;

/// <summary>
/// Simplified pointer events builder with fluent API for chaining pointer events rules.
/// </summary>
[TailwindPrefix("pointer-events-", Responsive = true)]
public sealed class PointerEventsBuilder : ICssBuilder
{
    private readonly List<PointerEventsRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // ----- Class constants -----
    private const string _classNone = "pointer-events-none";
    private const string _classAuto = "pointer-events-auto";

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
    /// Applies the pointer events on phone breakpoint.
    /// </summary>
    public PointerEventsBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the pointer events on small breakpoint (≥640px).
    /// </summary>
    public PointerEventsBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);

    /// <summary>
    /// Applies the pointer events on tablet breakpoint.
    /// </summary>
    public PointerEventsBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the pointer events on laptop breakpoint.
    /// </summary>
    public PointerEventsBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the pointer events on desktop breakpoint.
    /// </summary>
    public PointerEventsBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the pointer events on the 2xl breakpoint.
    /// </summary>
    public PointerEventsBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder Chain(string value)
    {
        _rules.Add(new PointerEventsRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PointerEventsBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
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
        return string.Empty;
    }

    public override string ToString() => ToClass();
}
