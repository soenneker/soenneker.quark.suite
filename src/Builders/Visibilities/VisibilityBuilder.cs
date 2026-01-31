using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified visibility builder with fluent API for chaining visibility rules.
/// </summary>
public sealed class VisibilityBuilder : ICssBuilder
{
    private readonly List<VisibilityRule> _rules = new(4);

    private const string _classInvisible = "invisible";
    private const string _classVisible = "visible";

    internal VisibilityBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new VisibilityRule(value, breakpoint));
    }

    internal VisibilityBuilder(List<VisibilityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the visibility to visible.
	/// </summary>
    public VisibilityBuilder Visible => Chain(VisibilityKeyword.VisibleValue);
	/// <summary>
	/// Sets the visibility to invisible.
	/// </summary>
    public VisibilityBuilder Invisible => Chain("invisible");
	/// <summary>
	/// Sets the visibility to inherit.
	/// </summary>
    public VisibilityBuilder Inherit => Chain(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the visibility to initial.
	/// </summary>
    public VisibilityBuilder Initial => Chain(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the visibility to revert.
	/// </summary>
    public VisibilityBuilder Revert => Chain(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the visibility to revert-layer.
	/// </summary>
    public VisibilityBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the visibility to unset.
	/// </summary>
    public VisibilityBuilder Unset => Chain(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the visibility on phone breakpoint.
	/// </summary>
    public VisibilityBuilder OnPhone => ChainBp(BreakpointType.Phone);
	/// <summary>
	/// Applies the visibility on tablet breakpoint.
	/// </summary>
    public VisibilityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
	/// <summary>
	/// Applies the visibility on laptop breakpoint.
	/// </summary>
    public VisibilityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
	/// <summary>
	/// Applies the visibility on desktop breakpoint.
	/// </summary>
    public VisibilityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
	/// <summary>
	/// Applies the visibility on widescreen breakpoint.
	/// </summary>
    public VisibilityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
	/// <summary>
	/// Applies the visibility on ultrawide breakpoint.
	/// </summary>
    public VisibilityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VisibilityBuilder Chain(string value)
    {
        _rules.Add(new VisibilityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VisibilityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new VisibilityRule(VisibilityKeyword.VisibleValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new VisibilityRule(last.Value, bp);
        return this;
    }

	/// <summary>
	/// Gets the CSS class string for the current configuration.
	/// </summary>
	/// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                "invisible" => _classInvisible,
                VisibilityKeyword.VisibleValue => _classVisible,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var css = rule.Value switch
            {
                "invisible" => "visibility: hidden",
                VisibilityKeyword.VisibleValue => "visibility: visible",
                GlobalKeyword.InheritValue => "visibility: inherit",
                GlobalKeyword.InitialValue => "visibility: initial",
                GlobalKeyword.UnsetValue => "visibility: unset",
                GlobalKeyword.RevertValue => "visibility: revert",
                GlobalKeyword.RevertLayerValue => "visibility: revert-layer",
                _ => null
            };
            if (css is null) continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
