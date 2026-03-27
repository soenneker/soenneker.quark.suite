using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Inset (top/right/bottom/left) builder with fluent API. Tailwind: inset-*, top-*, right-*, bottom-*, left-*, start-*, end-*.
/// </summary>
[TailwindPrefix("inset-", Responsive = true)]
public sealed class InsetBuilder : ICssBuilder
{
    private readonly List<InsetRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    private const string _baseToken = "inset";
    private const string _token0 = "0";
    private const string _token1 = "1";
    private const string _token2 = "2";
    private const string _token3 = "3";
    private const string _token4 = "4";
    private const string _token5 = "5";
    private const string _tokenAuto = "auto";
    private const string _tokenPx = "px";

    internal InsetBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new InsetRule(size, ElementSideType.All, breakpoint));
    }

    internal InsetBuilder(List<InsetRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public InsetBuilder FromTop => AddRule(ElementSideType.Top);
    public InsetBuilder FromRight => AddRule(ElementSideType.Right);
    public InsetBuilder FromBottom => AddRule(ElementSideType.Bottom);
    public InsetBuilder FromLeft => AddRule(ElementSideType.Left);
    public InsetBuilder OnX => AddRule(ElementSideType.Horizontal);
    public InsetBuilder OnY => AddRule(ElementSideType.Vertical);
    public InsetBuilder OnAll => AddRule(ElementSideType.All);
    public InsetBuilder FromStart => AddRule(ElementSideType.InlineStart);
    public InsetBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    public InsetBuilder Is0 => ChainWithSize(ScaleType.Is0);
    public InsetBuilder Is1 => ChainWithSize(ScaleType.Is1);
    public InsetBuilder Is2 => ChainWithSize(ScaleType.Is2);
    public InsetBuilder Is3 => ChainWithSize(ScaleType.Is3);
    public InsetBuilder Is4 => ChainWithSize(ScaleType.Is4);
    public InsetBuilder Is5 => ChainWithSize(ScaleType.Is5);
    public InsetBuilder Px => ChainWithSize(_tokenPx);
    public InsetBuilder Auto => ChainWithSize("auto");

    public InsetBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public InsetBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public InsetBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public InsetBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public InsetBuilder OnXxl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InsetBuilder AddRule(ElementSideType side)
    {
        var pending = ConsumePendingBreakpoint();
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        var bp = pending ?? (_rules.Count > 0 ? _rules[^1].Breakpoint : null);
        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
            _rules[^1] = new InsetRule(size, side, bp);
        else
            _rules.Add(new InsetRule(size, side, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InsetBuilder ChainWithSize(string size)
    {
        _rules.Add(new InsetRule(size, ElementSideType.All, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InsetBuilder ChainWithSize(ScaleType scale)
    {
        _rules.Add(new InsetRule(scale.Value, ElementSideType.All, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private InsetBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var sidePrefix = GetInsetSidePrefix(rule.Side);
            if (sidePrefix.Length == 0) continue;
            var cls = sidePrefix + "-" + sizeTok;
            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
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
            var (prop, val) = GetInsetStyle(rule.Side, sizeVal);
            if (prop is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append(prop);
            sb.Append(": ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeToken(string size) => size switch
    {
        ScaleType.Is0Value => _token0,
        ScaleType.Is1Value => _token1,
        ScaleType.Is2Value => _token2,
        ScaleType.Is3Value => _token3,
        ScaleType.Is4Value => _token4,
        ScaleType.Is5Value => _token5,
        "auto" => _tokenAuto,
        "px" => _tokenPx,
        _ => string.Empty
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetInsetSidePrefix(ElementSideType side)
    {
        if (side == ElementSideType.All) return _baseToken;
        if (side == ElementSideType.Top) return "top";
        if (side == ElementSideType.Right) return "right";
        if (side == ElementSideType.Bottom) return "bottom";
        if (side == ElementSideType.Left) return "left";
        if (side == ElementSideType.Horizontal || side == ElementSideType.LeftRight) return "inset-x";
        if (side == ElementSideType.Vertical || side == ElementSideType.TopBottom) return "inset-y";
        if (side == ElementSideType.InlineStart) return "start";
        if (side == ElementSideType.InlineEnd) return "end";
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size) => size switch
    {
        ScaleType.Is0Value => "0",
        ScaleType.Is1Value => "0.25rem",
        ScaleType.Is2Value => "0.5rem",
        ScaleType.Is3Value => "1rem",
        ScaleType.Is4Value => "1.5rem",
        ScaleType.Is5Value => "3rem",
        "auto" => "auto",
        "px" => "1px",
        _ => size
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (string? prop, string? val) GetInsetStyle(ElementSideType side, string sizeVal)
    {
        string? prop = null;
        if (side == ElementSideType.All) prop = "inset";
        else if (side == ElementSideType.Top) prop = "top";
        else if (side == ElementSideType.Right) prop = "right";
        else if (side == ElementSideType.Bottom) prop = "bottom";
        else if (side == ElementSideType.Left) prop = "left";
        else if (side == ElementSideType.Horizontal || side == ElementSideType.LeftRight) prop = "inset-inline";
        else if (side == ElementSideType.Vertical || side == ElementSideType.TopBottom) prop = "inset-block";
        else if (side == ElementSideType.InlineStart) prop = "inset-inline-start";
        else if (side == ElementSideType.InlineEnd) prop = "inset-inline-end";
        return (prop, sizeVal);
    }
}
