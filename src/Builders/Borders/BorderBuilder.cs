using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified border builder with fluent API for chaining border rules.
/// </summary>
public sealed class BorderBuilder : ICssBuilder
{
    private readonly List<BorderRule> _rules = new(4);

    // ----- Class tokens -----
    private const string _baseToken = "b";

    // ----- Side tokens -----
    private const string _sideT = "t";
    private const string _sideE = "e";
    private const string _sideB = "b";
    private const string _sideS = "s";
    private const string _sideX = "x";
    private const string _sideY = "y";

    internal BorderBuilder(string size, BreakpointType? breakpoint = null)
    {
        if (!string.IsNullOrEmpty(size))
            _rules.Add(new BorderRule(size, ElementSideType.All, breakpoint));
    }

    internal BorderBuilder(List<BorderRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Side chaining -----
    public BorderBuilder FromTop => AddRule(ElementSideType.Top);
    public BorderBuilder FromRight => AddRule(ElementSideType.Right);
    public BorderBuilder FromBottom => AddRule(ElementSideType.Bottom);
    public BorderBuilder FromLeft => AddRule(ElementSideType.Left);
    public BorderBuilder OnX => AddRule(ElementSideType.Horizontal);
    public BorderBuilder OnY => AddRule(ElementSideType.Vertical);
    public BorderBuilder OnAll => AddRule(ElementSideType.All);
    public BorderBuilder FromStart => AddRule(ElementSideType.InlineStart);
    public BorderBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    // ----- Size chaining -----
    public BorderBuilder S0 => ChainWithSize(ScaleType.S0);
    public BorderBuilder S1 => ChainWithSize(ScaleType.S1);
    public BorderBuilder S2 => ChainWithSize(ScaleType.S2);
    public BorderBuilder S3 => ChainWithSize(ScaleType.S3);
    public BorderBuilder S4 => ChainWithSize(ScaleType.S4);
    public BorderBuilder S5 => ChainWithSize(ScaleType.S5);

    // ----- BreakpointType chaining -----
    public BorderBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public BorderBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public BorderBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public BorderBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public BorderBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public BorderBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderBuilder AddRule(ElementSideType side)
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : "0";
        var bp = _rules.Count > 0 ? _rules[^1].breakpoint : null;

        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
        {
            _rules[^1] = new BorderRule(size, side, bp);
        }
        else
        {
            _rules.Add(new BorderRule(size, side, bp));
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderBuilder ChainWithSize(ScaleType scale)
    {
        _rules.Add(new BorderRule(scale.Value, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BorderRule("0", ElementSideType.All, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BorderRule(last.Size, last.Side, breakpoint);
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

            var sizeTok = rule.Size;

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
                        AppendStyle(ref first, ref sb, "border-width", sizeVal);
                        break;

                    case ElementSideType.TopValue:
                        AppendStyle(ref first, ref sb, "border-top-width", sizeVal);
                        break;

                    case ElementSideType.RightValue:
                        AppendStyle(ref first, ref sb, "border-right-width", sizeVal);
                        break;

                    case ElementSideType.BottomValue:
                        AppendStyle(ref first, ref sb, "border-bottom-width", sizeVal);
                        break;

                    case ElementSideType.LeftValue:
                        AppendStyle(ref first, ref sb, "border-left-width", sizeVal);
                        break;

                    case ElementSideType.HorizontalValue:
                    case ElementSideType.LeftRightValue:
                        AppendStyle(ref first, ref sb, "border-left-width", sizeVal);
                        AppendStyle(ref first, ref sb, "border-right-width", sizeVal);
                        break;

                    case ElementSideType.VerticalValue:
                    case ElementSideType.TopBottomValue:
                        AppendStyle(ref first, ref sb, "border-top-width", sizeVal);
                        AppendStyle(ref first, ref sb, "border-bottom-width", sizeVal);
                        break;

                    case ElementSideType.InlineStartValue:
                        AppendStyle(ref first, ref sb, "border-inline-start-width", sizeVal);
                        break;

                    case ElementSideType.InlineEndValue:
                        AppendStyle(ref first, ref sb, "border-inline-end-width", sizeVal);
                        break;

                    default:
                        AppendStyle(ref first, ref sb, "border-width", sizeVal);
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
        if (!first) 
            sb.Append("; ");
        else 
            first = false;

        sb.Append(prop);
        sb.Append(": ");
        sb.Append(val);
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
            ScaleType.S1Value => "1px",
            ScaleType.S2Value => "2px",
            ScaleType.S3Value => "3px",
            ScaleType.S4Value => "4px",
            ScaleType.S5Value => "5px",
            _ => null
        };
    }

}
