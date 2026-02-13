using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified object position builder with fluent API for chaining object position rules.
/// </summary>
public sealed class ObjectPositionBuilder : ICssBuilder
{
    private readonly List<ObjectPositionRule> _rules = new(4);

    private const string _classObjectPositionCenter = "object-center";
    private const string _classObjectPositionTop = "object-top";
    private const string _classObjectPositionRight = "object-right";
    private const string _classObjectPositionBottom = "object-bottom";
    private const string _classObjectPositionLeft = "object-left";
    private const string _classObjectPositionTopLeft = "object-top-left";
    private const string _classObjectPositionTopRight = "object-top-right";
    private const string _classObjectPositionBottomLeft = "object-bottom-left";
    private const string _classObjectPositionBottomRight = "object-bottom-right";

    internal ObjectPositionBuilder(string position, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ObjectPositionRule(position, breakpoint));
    }

    internal ObjectPositionBuilder(List<ObjectPositionRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the object position to center.
    /// </summary>
    public ObjectPositionBuilder Center => ChainWithPosition("center");
    /// <summary>
    /// Sets the object position to top.
    /// </summary>
    public ObjectPositionBuilder Top => ChainWithPosition("top");
    /// <summary>
    /// Sets the object position to right.
    /// </summary>
    public ObjectPositionBuilder Right => ChainWithPosition("right");
    /// <summary>
    /// Sets the object position to bottom.
    /// </summary>
    public ObjectPositionBuilder Bottom => ChainWithPosition("bottom");
    /// <summary>
    /// Sets the object position to left.
    /// </summary>
    public ObjectPositionBuilder Left => ChainWithPosition("left");
    /// <summary>
    /// Sets the object position to top-left.
    /// </summary>
    public ObjectPositionBuilder TopLeft => ChainWithPosition("top-left");
    /// <summary>
    /// Sets the object position to top-right.
    /// </summary>
    public ObjectPositionBuilder TopRight => ChainWithPosition("top-right");
    /// <summary>
    /// Sets the object position to bottom-left.
    /// </summary>
    public ObjectPositionBuilder BottomLeft => ChainWithPosition("bottom-left");
    /// <summary>
    /// Sets the object position to bottom-right.
    /// </summary>
    public ObjectPositionBuilder BottomRight => ChainWithPosition("bottom-right");

    /// <summary>
    /// Applies the object position on phone breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the object position on tablet breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the object position on laptop breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the object position on desktop breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the object position on widescreen breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the object position on ultrawide breakpoint.
    /// </summary>
    public ObjectPositionBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ObjectPositionBuilder ChainWithPosition(string position)
    {
        _rules.Add(new ObjectPositionRule(position, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ObjectPositionBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ObjectPositionRule("center", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ObjectPositionRule last = _rules[lastIdx];
        _rules[lastIdx] = new ObjectPositionRule(last.Position, breakpoint);
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
            ObjectPositionRule rule = _rules[i];
            string cls = GetObjectPositionClass(rule.Position);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            ObjectPositionRule rule = _rules[i];
            string? positionValue = GetObjectPositionValue(rule.Position);

            if (positionValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("object-position: ");
            sb.Append(positionValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetObjectPositionClass(string position)
    {
        return position switch
        {
            "center" => _classObjectPositionCenter,
            "top" => _classObjectPositionTop,
            "right" => _classObjectPositionRight,
            "bottom" => _classObjectPositionBottom,
            "left" => _classObjectPositionLeft,
            "top-left" => _classObjectPositionTopLeft,
            "top-right" => _classObjectPositionTopRight,
            "bottom-left" => _classObjectPositionBottomLeft,
            "bottom-right" => _classObjectPositionBottomRight,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetObjectPositionValue(string position)
    {
        return position switch
        {
            "center" => "center",
            "top" => "top",
            "right" => "right",
            "bottom" => "bottom",
            "left" => "left",
            "top-left" => "top left",
            "top-right" => "top right",
            "bottom-left" => "bottom left",
            "bottom-right" => "bottom right",
            _ => null
        };
    }

    /// <summary>
    /// Returns the CSS class string representation of this object position builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}
