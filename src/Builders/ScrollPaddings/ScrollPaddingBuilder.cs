using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll padding builder. Tailwind: scroll-p-*, scroll-pt-*, scroll-pr-*, etc.
/// </summary>
public sealed class ScrollPaddingBuilder : ICssBuilder
{
    private readonly List<ScrollPaddingRule> _rules = new(4);

    private const string _baseToken = "scroll-p";
    private const string _sideT = "t";
    private const string _sideE = "e";
    private const string _sideB = "b";
    private const string _sideS = "s";
    private const string _sideX = "x";
    private const string _sideY = "y";

    internal ScrollPaddingBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollPaddingRule(size, ElementSideType.All, breakpoint));
    }

    internal ScrollPaddingBuilder(List<ScrollPaddingRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScrollPaddingBuilder FromTop => AddRule(ElementSideType.Top);
    public ScrollPaddingBuilder FromRight => AddRule(ElementSideType.Right);
    public ScrollPaddingBuilder FromBottom => AddRule(ElementSideType.Bottom);
    public ScrollPaddingBuilder FromLeft => AddRule(ElementSideType.Left);
    public ScrollPaddingBuilder OnX => AddRule(ElementSideType.Horizontal);
    public ScrollPaddingBuilder OnY => AddRule(ElementSideType.Vertical);
    public ScrollPaddingBuilder OnAll => AddRule(ElementSideType.All);
    public ScrollPaddingBuilder FromStart => AddRule(ElementSideType.InlineStart);
    public ScrollPaddingBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    public ScrollPaddingBuilder Is0 => ChainWithSize(ScaleType.Is0);
    public ScrollPaddingBuilder Is1 => ChainWithSize(ScaleType.Is1);
    public ScrollPaddingBuilder Is2 => ChainWithSize(ScaleType.Is2);
    public ScrollPaddingBuilder Is3 => ChainWithSize(ScaleType.Is3);
    public ScrollPaddingBuilder Is4 => ChainWithSize(ScaleType.Is4);
    public ScrollPaddingBuilder Is5 => ChainWithSize(ScaleType.Is5);
    public ScrollPaddingBuilder Px => ChainWithSize("px");

    public ScrollPaddingBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    public ScrollPaddingBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    public ScrollPaddingBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    public ScrollPaddingBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    public ScrollPaddingBuilder OnXxl => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollPaddingBuilder AddRule(ElementSideType side)
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        var bp = _rules.Count > 0 ? _rules[^1].Breakpoint : null;
        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
            _rules[^1] = new ScrollPaddingRule(size, side, bp);
        else
            _rules.Add(new ScrollPaddingRule(size, side, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollPaddingBuilder ChainWithSize(string size)
    {
        _rules.Add(new ScrollPaddingRule(size, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollPaddingBuilder ChainWithSize(ScaleType scale)
    {
        _rules.Add(new ScrollPaddingRule(scale.Value, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollPaddingBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
            _rules.Add(new ScrollPaddingRule(ScaleType.Is0Value, ElementSideType.All, breakpoint));
        else
        {
            var last = _rules[^1];
            _rules[^1] = new ScrollPaddingRule(last.Size, last.Side, breakpoint);
        }
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var sizeTok = GetSizeToken(rule.Size);
            if (sizeTok.Length == 0) continue;
            var sideTok = GetSideToken(rule.Side);
            var baseClass = _baseToken + sideTok + "-" + sizeTok;
            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0) baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(baseClass);
        }
        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var sizeVal = GetSizeValue(rule.Size);
            if (sizeVal is null) continue;
            string prop;
            switch (rule.Side)
            {
                case ElementSideType.AllValue: prop = "scroll-padding"; break;
                case ElementSideType.TopValue: prop = "scroll-padding-top"; break;
                case ElementSideType.RightValue: prop = "scroll-padding-right"; break;
                case ElementSideType.BottomValue: prop = "scroll-padding-bottom"; break;
                case ElementSideType.LeftValue: prop = "scroll-padding-left"; break;
                case ElementSideType.HorizontalValue:
                case ElementSideType.LeftRightValue: prop = "scroll-padding-inline"; break;
                case ElementSideType.VerticalValue:
                case ElementSideType.TopBottomValue: prop = "scroll-padding-block"; break;
                case ElementSideType.InlineStartValue: prop = "scroll-padding-inline-start"; break;
                case ElementSideType.InlineEndValue: prop = "scroll-padding-inline-end"; break;
                default: prop = "scroll-padding"; break;
            }
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append(prop);
            sb.Append(": ");
            sb.Append(sizeVal);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetSizeToken(string size) => size switch
    {
        ScaleType.Is0Value => "0",
        ScaleType.Is1Value => "1",
        ScaleType.Is2Value => "2",
        ScaleType.Is3Value => "3",
        ScaleType.Is4Value => "4",
        ScaleType.Is5Value => "5",
        "px" => "px",
        _ => string.Empty
    };

    private static string GetSideToken(ElementSideType side)
    {
        switch (side)
        {
            case ElementSideType.AllValue: return string.Empty;
            case ElementSideType.TopValue: return _sideT;
            case ElementSideType.RightValue: return _sideE;
            case ElementSideType.BottomValue: return _sideB;
            case ElementSideType.LeftValue: return _sideS;
            case ElementSideType.HorizontalValue:
            case ElementSideType.LeftRightValue: return _sideX;
            case ElementSideType.VerticalValue:
            case ElementSideType.TopBottomValue: return _sideY;
            case ElementSideType.InlineStartValue: return _sideS;
            case ElementSideType.InlineEndValue: return _sideE;
            default: return string.Empty;
        }
    }

    private static string? GetSizeValue(string size) => size switch
    {
        ScaleType.Is0Value => "0",
        ScaleType.Is1Value => "0.25rem",
        ScaleType.Is2Value => "0.5rem",
        ScaleType.Is3Value => "1rem",
        ScaleType.Is4Value => "1.5rem",
        ScaleType.Is5Value => "3rem",
        "px" => "1px",
        _ => null
    };
}
