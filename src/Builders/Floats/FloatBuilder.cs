using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified float builder with fluent API for chaining float rules.
/// </summary>
public sealed class FloatBuilder : ICssBuilder
{
    private readonly List<FloatRule> _rules = new(4);

    // Tailwind float utilities (for Quark Suite / shadcn). Start/End for RTL; Left/Right map to same.
    private const string _classStart = "float-start";
    private const string _classEnd = "float-end";
    private const string _classNone = "float-none";

    private const string _floatPrefix = "float: ";

    internal FloatBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FloatRule(value, breakpoint));
    }

    internal FloatBuilder(List<FloatRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the float to none.
	/// </summary>
    public FloatBuilder None => ChainWithValue(FloatKeyword.NoneValue);
	/// <summary>
	/// Sets the float to left.
	/// </summary>
    public FloatBuilder Left => ChainWithValue(FloatKeyword.LeftValue);
	/// <summary>
	/// Sets the float to right.
	/// </summary>
    public FloatBuilder Right => ChainWithValue(FloatKeyword.RightValue);
	/// <summary>
	/// Sets the float to start (inline-start).
	/// </summary>
    public FloatBuilder Start => ChainWithValue(FloatKeyword.InlineStartValue);
	/// <summary>
	/// Sets the float to end (inline-end).
	/// </summary>
    public FloatBuilder End => ChainWithValue(FloatKeyword.InlineEndValue);
	/// <summary>
	/// Sets the float to inherit.
	/// </summary>
    public FloatBuilder Inherit => ChainWithValue(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the float to initial.
	/// </summary>
    public FloatBuilder Initial => ChainWithValue(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the float to revert.
	/// </summary>
    public FloatBuilder Revert => ChainWithValue(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the float to revert-layer.
	/// </summary>
    public FloatBuilder RevertLayer => ChainWithValue(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the float to unset.
	/// </summary>
    public FloatBuilder Unset => ChainWithValue(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the float on phone breakpoint.
	/// </summary>
    public FloatBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the float on tablet breakpoint.
	/// </summary>
    public FloatBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the float on laptop breakpoint.
	/// </summary>
    public FloatBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the float on desktop breakpoint.
	/// </summary>
    public FloatBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the float on widescreen breakpoint.
	/// </summary>
    public FloatBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
	/// <summary>
	/// Applies the float on ultrawide breakpoint.
	/// </summary>
    public FloatBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder ChainWithValue(string value)
    {
        _rules.Add(new FloatRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FloatRule(FloatKeyword.NoneValue, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new FloatRule(last.Value, breakpoint);
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

            var cls = rule.Value switch
            {
                FloatKeyword.LeftValue => _classStart,
                FloatKeyword.RightValue => _classEnd,
                FloatKeyword.InlineStartValue => _classStart,
                FloatKeyword.InlineEndValue => _classEnd,
                FloatKeyword.NoneValue => _classNone,
                _ => string.Empty
            };

            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            var rule = _rules[i];

            var css = rule.Value switch
            {
                FloatKeyword.LeftValue => "left",
                FloatKeyword.RightValue => "right",
                FloatKeyword.InlineStartValue => "inline-start",
                FloatKeyword.InlineEndValue => "inline-end",
                FloatKeyword.NoneValue => "none",
                GlobalKeyword.InheritValue => "inherit",
                GlobalKeyword.InitialValue => "initial",
                GlobalKeyword.UnsetValue => "unset",
                GlobalKeyword.RevertValue => "revert",
                GlobalKeyword.RevertLayerValue => "revert-layer",
                _ => null
            };

            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_floatPrefix);
            sb.Append(css);
        }

        return sb.ToString();
    }


}
