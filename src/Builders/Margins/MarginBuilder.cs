using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified margin builder with fluent API for chaining margin rules.
/// </summary>
public sealed class MarginBuilder : ICssBuilder
{
    private readonly List<MarginRule> _rules = new(4);

    // ----- Class tokens -----
    private const string _baseToken = "m";

    // ----- Size tokens -----
    private const string _token0 = "0";
    private const string _token1 = "1";
    private const string _token2 = "2";
    private const string _token3 = "3";
    private const string _token4 = "4";
    private const string _token5 = "5";
    private const string _tokenAuto = "auto";

    // ----- Side tokens (Bootstrap naming) -----
    private const string _sideT = "t";
    private const string _sideE = "e";
    private const string _sideB = "b";
    private const string _sideS = "s";
    private const string _sideX = "x";
    private const string _sideY = "y";

    internal MarginBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new MarginRule(size, ElementSideType.All, breakpoint));
    }

    internal MarginBuilder(List<MarginRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Side chaining -----
    public MarginBuilder FromTop => AddRule(ElementSideType.Top);
    public MarginBuilder FromRight => AddRule(ElementSideType.Right);
    public MarginBuilder FromBottom => AddRule(ElementSideType.Bottom);
    public MarginBuilder FromLeft => AddRule(ElementSideType.Left);
    public MarginBuilder OnX => AddRule(ElementSideType.Horizontal);
    public MarginBuilder OnY => AddRule(ElementSideType.Vertical);
    public MarginBuilder OnAll => AddRule(ElementSideType.All);
    public MarginBuilder FromStart => AddRule(ElementSideType.InlineStart);
    public MarginBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    // ----- Size chaining -----
    public MarginBuilder S0 => ChainWithSize(ScaleType.S0);
    public MarginBuilder S1 => ChainWithSize(ScaleType.S1);
    public MarginBuilder S2 => ChainWithSize(ScaleType.S2);
    public MarginBuilder S3 => ChainWithSize(ScaleType.S3);
    public MarginBuilder S4 => ChainWithSize(ScaleType.S4);
    public MarginBuilder S5 => ChainWithSize(ScaleType.S5);
    public MarginBuilder Auto => ChainWithSize("auto");

    // ----- BreakpointType chaining -----
    public MarginBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public MarginBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public MarginBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public MarginBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public MarginBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public MarginBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MarginBuilder AddRule(ElementSideType side)
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.S0Value;
        var bp = _rules.Count > 0 ? _rules[^1].breakpoint : null;

        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
        {
            _rules[^1] = new MarginRule(size, side, bp);
        }
        else
        {
            _rules.Add(new MarginRule(size, side, bp));
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MarginBuilder ChainWithSize(string size)
    {
        _rules.Add(new MarginRule(size, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MarginBuilder ChainWithSize(ScaleType scale)
    {
        _rules.Add(new MarginRule(scale.Value, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MarginBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new MarginRule(ScaleType.S0Value, ElementSideType.All, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new MarginRule(last.Size, last.Side, breakpoint);
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

            var sizeTok = GetSizeToken(rule.Size);

            if (sizeTok.Length == 0)
                continue;

            var sideTok = GetSideToken(rule.Side);
            var bpTok = BreakpointUtil.GetBreakpointToken(rule.breakpoint);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(_baseToken);

            if (sideTok.Length != 0)
                sb.Append(sideTok);

            sb.Append('-');

            if (bpTok.Length != 0)
            {
                sb.Append(bpTok);
                sb.Append('-');
            }

            sb.Append(sizeTok);
        }

        return sb.ToString();
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        var sb = new PooledStringBuilder();
        var first = true;
        try
        {
            for (var i = 0; i < _rules.Count; i++)
            {
                var rule = _rules[i];
                var sizeVal = GetSizeValue(rule.Size);
                if (sizeVal is null)
                    continue;

                switch (rule.Side)
                {
                    case ElementSideType.AllValue:
                        AppendStyle(ref first, ref sb, "margin", sizeVal);
                        break;

                    case ElementSideType.TopValue:
                        AppendStyle(ref first, ref sb, "margin-top", sizeVal);
                        break;

                    case ElementSideType.RightValue:
                        AppendStyle(ref first, ref sb, "margin-right", sizeVal);
                        break;

                    case ElementSideType.BottomValue:
                        AppendStyle(ref first, ref sb, "margin-bottom", sizeVal);
                        break;

                    case ElementSideType.LeftValue:
                        AppendStyle(ref first, ref sb, "margin-left", sizeVal);
                        break;

                    case ElementSideType.HorizontalValue:
                    case ElementSideType.LeftRightValue:
                        AppendStyle(ref first, ref sb, "margin-left", sizeVal);
                        AppendStyle(ref first, ref sb, "margin-right", sizeVal);
                        break;

                    case ElementSideType.VerticalValue:
                    case ElementSideType.TopBottomValue:
                        AppendStyle(ref first, ref sb, "margin-top", sizeVal);
                        AppendStyle(ref first, ref sb, "margin-bottom", sizeVal);
                        break;

                    case ElementSideType.InlineStartValue:
                        AppendStyle(ref first, ref sb, "margin-inline-start", sizeVal);
                        break;

                    case ElementSideType.InlineEndValue:
                        AppendStyle(ref first, ref sb, "margin-inline-end", sizeVal);
                        break;

                    default:
                        AppendStyle(ref first, ref sb, "margin", sizeVal);
                        break;
                }
            }

            return sb.ToString();
        }
        finally
        {
            sb.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void AppendStyle(ref bool first, ref PooledStringBuilder sb, string prop, string val)
        {
            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(prop);
            sb.Append(": ");
            sb.Append(val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetSizeToken(string size)
        {
            return size switch
            {
                ScaleType.S0Value => _token0,
                ScaleType.S1Value => _token1,
                ScaleType.S2Value => _token2,
                ScaleType.S3Value => _token3,
                ScaleType.S4Value => _token4,
                ScaleType.S5Value => _token5,
                "auto" => _tokenAuto,
                _ => string.Empty
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetSideToken(ElementSideType side)
        {
            switch (side)
            {
                case ElementSideType.AllValue:
                    return string.Empty;
                case ElementSideType.TopValue:
                    return _sideT;
                case ElementSideType.RightValue:
                    return _sideE;
                case ElementSideType.BottomValue:
                    return _sideB;
                case ElementSideType.LeftValue:
                    return _sideS;
                case ElementSideType.HorizontalValue:
                case ElementSideType.LeftRightValue:
                    return _sideX;
                case ElementSideType.VerticalValue:
                case ElementSideType.TopBottomValue:
                    return _sideY;
                case ElementSideType.InlineStartValue:
                    return _sideS;
                case ElementSideType.InlineEndValue:
                    return _sideE;
                default:
                    return string.Empty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string? GetSizeValue(string size)
        {
            return size switch
            {
                ScaleType.S0Value => "0",
                ScaleType.S1Value => "0.25rem",
                ScaleType.S2Value => "0.5rem",
                ScaleType.S3Value => "1rem",
                ScaleType.S4Value => "1.5rem",
                ScaleType.S5Value => "3rem",
                "auto" => "auto",
                _ => null
            };
        }

    }
