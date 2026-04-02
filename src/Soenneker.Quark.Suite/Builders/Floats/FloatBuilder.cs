using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified float builder with fluent API for chaining float rules.
/// </summary>
[TailwindPrefix("float-", Responsive = true)]
public sealed class FloatBuilder : ICssBuilder
{
    private readonly List<FloatRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind float utilities (for Quark Suite / shadcn). Start/End for RTL; Left/Right map to same.
    private const string _classStart = "float-start";
    private const string _classEnd = "float-end";
    private const string _classNone = "float-none";

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
	/// Applies the float on phone breakpoint.
	/// </summary>
    public FloatBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the float on small breakpoint (≥640px).
	/// </summary>
    public FloatBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
	/// <summary>
	/// Applies the float on tablet breakpoint.
	/// </summary>
    public FloatBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the float on laptop breakpoint.
	/// </summary>
    public FloatBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the float on desktop breakpoint.
	/// </summary>
    public FloatBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the float on the 2xl breakpoint.
	/// </summary>
    public FloatBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder ChainWithValue(string value)
    {
        _rules.Add(new FloatRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FloatBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
        return string.Empty;
    }
    public override string ToString() => ToClass();
}