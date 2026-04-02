using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance box shadow builder with fluent API for chaining box shadow rules.
/// </summary>
[TailwindPrefix("shadow-", Responsive = true)]
public sealed class BoxShadowBuilder : ICssBuilder
{
    private readonly List<BoxShadowRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // ----- Class name constants -----
    private const string _classNone = "shadow-none";
    private const string _classBase = "shadow";
    private const string _classSm = "shadow-sm";
    private const string _classLg = "shadow-lg";

    internal BoxShadowBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BoxShadowRule(value, breakpoint));
    }

    internal BoxShadowBuilder(List<BoxShadowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the box shadow to none.
    /// </summary>
    public BoxShadowBuilder None => Chain("none");
    /// <summary>
    /// Sets the box shadow to base.
    /// </summary>
    public BoxShadowBuilder Base => Chain("base");
    /// <summary>
    /// Sets the box shadow to small.
    /// </summary>
    public BoxShadowBuilder Small => Chain(SizeType.Small.Value);
    /// <summary>
    /// Sets the box shadow to large.
    /// </summary>
    public BoxShadowBuilder Large => Chain(SizeType.Large.Value);

    /// <summary>
    /// Applies the box shadow on phone breakpoint.
    /// </summary>
    public BoxShadowBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the box shadow on small breakpoint (≥640px).
    /// </summary>
    public BoxShadowBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the box shadow on tablet breakpoint.
    /// </summary>
    public BoxShadowBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the box shadow on laptop breakpoint.
    /// </summary>
    public BoxShadowBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the box shadow on desktop breakpoint.
    /// </summary>
    public BoxShadowBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the box shadow on the 2xl breakpoint.
    /// </summary>
    public BoxShadowBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new BoxShadowRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
                "none" => _classNone,
                "base" => _classBase,
                "sm" => _classSm,
                "lg" => _classLg,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Shadow utilities are class-first; no inline style mapping.
    /// </summary>
    /// <returns>An empty string as shadow utilities are class-first.</returns>
    public string ToStyle()
    {
        // Shadow utilities are class-first; no inline style mapping
        return string.Empty;
    }


}