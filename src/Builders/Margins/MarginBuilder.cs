using Soenneker.Quark.Enums;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    // ----- Side tokens (Tailwind: t=top, e=end, b=bottom, s=start, x=horizontal, y=vertical) -----
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

	/// <summary>
	/// Applies margin from the top side.
	/// </summary>
    public MarginBuilder FromTop => AddRule(ElementSideType.Top);
	/// <summary>
	/// Applies margin from the right side.
	/// </summary>
    public MarginBuilder FromRight => AddRule(ElementSideType.Right);
	/// <summary>
	/// Applies margin from the bottom side.
	/// </summary>
    public MarginBuilder FromBottom => AddRule(ElementSideType.Bottom);
	/// <summary>
	/// Applies margin from the left side.
	/// </summary>
    public MarginBuilder FromLeft => AddRule(ElementSideType.Left);
	/// <summary>
	/// Applies margin on the horizontal axis (left and right).
	/// </summary>
    public MarginBuilder OnX => AddRule(ElementSideType.Horizontal);
	/// <summary>
	/// Applies margin on the vertical axis (top and bottom).
	/// </summary>
    public MarginBuilder OnY => AddRule(ElementSideType.Vertical);
	/// <summary>
	/// Applies margin on all sides.
	/// </summary>
    public MarginBuilder OnAll => AddRule(ElementSideType.All);
	/// <summary>
	/// Applies margin from the inline start.
	/// </summary>
    public MarginBuilder FromStart => AddRule(ElementSideType.InlineStart);
	/// <summary>
	/// Applies margin from the inline end.
	/// </summary>
    public MarginBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

	/// <summary>
	/// Sets the margin size to 0.
	/// </summary>
    public MarginBuilder Is0 => ChainWithSize(ScaleType.Is0);
	/// <summary>
	/// Sets the margin size to 1.
	/// </summary>
    public MarginBuilder Is1 => ChainWithSize(ScaleType.Is1);
	/// <summary>
	/// Sets the margin size to 2.
	/// </summary>
    public MarginBuilder Is2 => ChainWithSize(ScaleType.Is2);
	/// <summary>
	/// Sets the margin size to 3.
	/// </summary>
    public MarginBuilder Is3 => ChainWithSize(ScaleType.Is3);
	/// <summary>
	/// Sets the margin size to 4.
	/// </summary>
    public MarginBuilder Is4 => ChainWithSize(ScaleType.Is4);
	/// <summary>
	/// Sets the margin size to 5.
	/// </summary>
    public MarginBuilder Is5 => ChainWithSize(ScaleType.Is5);
	/// <summary>
	/// Sets the margin to auto.
	/// </summary>
    public MarginBuilder Auto => ChainWithSize("auto");

	/// <summary>
	/// Applies the margin on phone breakpoint.
	/// </summary>
    public MarginBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the margin on tablet breakpoint.
	/// </summary>
    public MarginBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the margin on laptop breakpoint.
	/// </summary>
    public MarginBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the margin on desktop breakpoint.
	/// </summary>
    public MarginBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    public MarginBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    public MarginBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    public MarginBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the margin on widescreen breakpoint.
	/// </summary>
    public MarginBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
	/// <summary>
	/// Applies the margin on ultrawide breakpoint.
	/// </summary>
    public MarginBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MarginBuilder AddRule(ElementSideType side)
    {
        string? size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        BreakpointType? bp = _rules.Count > 0 ? _rules[^1].Breakpoint : null;

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
            _rules.Add(new MarginRule(ScaleType.Is0Value, ElementSideType.All, breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        MarginRule last = _rules[lastIdx];
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
            MarginRule rule = _rules[i];

            string sizeTok = GetSizeToken(rule.Size);

            if (sizeTok.Length == 0)
                continue;

            string sideTok = GetSideToken(rule.Side);
            string bpTok = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            // Tailwind: mt-1, md:mt-1 (not legacy mt-md-1 syntax)
            string baseClass = _baseToken + (sideTok.Length != 0 ? sideTok : "") + "-" + sizeTok;
            string cls = bpTok.Length != 0 ? BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bpTok) : baseClass;
            sb.Append(cls);
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
                MarginRule rule = _rules[i];
                string? sizeVal = GetSizeValue(rule.Size);
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
                ScaleType.Is0Value => _token0,
                ScaleType.Is1Value => _token1,
                ScaleType.Is2Value => _token2,
                ScaleType.Is3Value => _token3,
                ScaleType.Is4Value => _token4,
                ScaleType.Is5Value => _token5,
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
                ScaleType.Is0Value => "0",
                ScaleType.Is1Value => "0.25rem",
                ScaleType.Is2Value => "0.5rem",
                ScaleType.Is3Value => "1rem",
                ScaleType.Is4Value => "1.5rem",
                ScaleType.Is5Value => "3rem",
                "auto" => "auto",
                _ => size
            };
        }

    }
