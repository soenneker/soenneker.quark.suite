using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance position builder with fluent API for chaining position rules.
/// </summary>
public sealed class PositionBuilder : ICssBuilder
{
    private readonly List<PositionRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _classStatic = "position-static";
    private const string _classRelative = "position-relative";
    private const string _classAbsolute = "position-absolute";
    private const string _classFixed = "position-fixed";
    private const string _classSticky = "position-sticky";

    // ----- CSS prefix -----
    private const string _positionPrefix = "position: ";

    internal PositionBuilder(string position, BreakpointType? breakpoint = null)
    {
        _rules.Add(new PositionRule(position, breakpoint));
    }

    internal PositionBuilder(List<PositionRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>Chain with static positioning for the next rule.</summary>
    public PositionBuilder Static => ChainWithPosition(PositionKeyword.StaticValue);
    /// <summary>Chain with relative positioning for the next rule.</summary>
    public PositionBuilder Relative => ChainWithPosition(PositionKeyword.RelativeValue);
    /// <summary>Chain with absolute positioning for the next rule.</summary>
    public PositionBuilder Absolute => ChainWithPosition(PositionKeyword.AbsoluteValue);
    /// <summary>Chain with fixed positioning for the next rule.</summary>
    public PositionBuilder Fixed => ChainWithPosition(PositionKeyword.FixedValue);
    /// <summary>Chain with sticky positioning for the next rule.</summary>
    public PositionBuilder Sticky => ChainWithPosition(PositionKeyword.StickyValue);

	/// <summary>
	/// Chain with inherit positioning for the next rule.
	/// </summary>
    public PositionBuilder Inherit => ChainWithPosition(GlobalKeyword.InheritValue);
	/// <summary>
	/// Chain with initial positioning for the next rule.
	/// </summary>
    public PositionBuilder Initial => ChainWithPosition(GlobalKeyword.InitialValue);
	/// <summary>
	/// Chain with revert positioning for the next rule.
	/// </summary>
    public PositionBuilder Revert => ChainWithPosition(GlobalKeyword.RevertValue);
	/// <summary>
	/// Chain with revert-layer positioning for the next rule.
	/// </summary>
    public PositionBuilder RevertLayer => ChainWithPosition(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Chain with unset positioning for the next rule.
	/// </summary>
    public PositionBuilder Unset => ChainWithPosition(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the position on phone breakpoint.
	/// </summary>
    public PositionBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
	/// <summary>
	/// Applies the position on tablet breakpoint.
	/// </summary>
    public PositionBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
	/// <summary>
	/// Applies the position on laptop breakpoint.
	/// </summary>
    public PositionBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
	/// <summary>
	/// Applies the position on desktop breakpoint.
	/// </summary>
    public PositionBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
	/// <summary>
	/// Applies the position on widescreen breakpoint.
	/// </summary>
    public PositionBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
	/// <summary>
	/// Applies the position on ultrawide breakpoint.
	/// </summary>
    public PositionBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionBuilder ChainWithPosition(string position)
    {
        _rules.Add(new PositionRule(position, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "static" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new PositionRule(PositionKeyword.StaticValue, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new PositionRule(last.Position, breakpoint);
        return this;
    }

    /// <summary>Gets the CSS class string for the current configuration.</summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];

            var baseClass = GetPositionClass(rule.Position);
            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.InsertBreakpointType(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            if (rule.Position.Length == 0)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append(_positionPrefix);
            sb.Append(rule.Position);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetPositionClass(string position)
    {
        return position switch
        {
            // Intellenum<string> *Value constants are compile-time consts, safe in switch
            PositionKeyword.StaticValue => _classStatic,
            PositionKeyword.RelativeValue => _classRelative,
            PositionKeyword.AbsoluteValue => _classAbsolute,
            PositionKeyword.FixedValue => _classFixed,
            PositionKeyword.StickyValue => _classSticky,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}
