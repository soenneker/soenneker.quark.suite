using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;
using Soenneker.Quark.Attributes;


namespace Soenneker.Quark;

/// <summary>
/// High-performance position builder with fluent API for chaining position rules.
/// </summary>
[TailwindPrefix("", Responsive = true)]
public sealed class PositionBuilder : ICssBuilder
{
    private readonly List<PositionRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // ----- Class name constants -----
    private const string _classStatic = "static";
    private const string _classRelative = "relative";
    private const string _classAbsolute = "absolute";
    private const string _classFixed = "fixed";
    private const string _classSticky = "sticky";

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

    /// <summary>Applies the position on phone breakpoint.</summary>
    public PositionBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>Applies the position on small breakpoint (≥640px).</summary>
    public PositionBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>Applies the position on tablet breakpoint.</summary>
    public PositionBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>Applies the position on laptop breakpoint.</summary>
    public PositionBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>Applies the position on desktop breakpoint.</summary>
    public PositionBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>Applies the position on the 2xl breakpoint.</summary>
    public PositionBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionBuilder ChainWithPosition(string position)
    {
        _rules.Add(new PositionRule(position, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

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
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetPositionClass(string position)
    {
        return position switch
        {
            // EnumValue<string> *Value constants are compile-time consts, safe in switch
            PositionKeyword.StaticValue => _classStatic,
            PositionKeyword.RelativeValue => _classRelative,
            PositionKeyword.AbsoluteValue => _classAbsolute,
            PositionKeyword.FixedValue => _classFixed,
            PositionKeyword.StickyValue => _classSticky,
            _ => string.Empty
        };
    }

    public override string ToString() => ToClass();
}
