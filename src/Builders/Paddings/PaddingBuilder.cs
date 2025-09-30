using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance padding builder with fluent API for chaining padding rules.
/// </summary>
public sealed class PaddingBuilder : ICssBuilder
{
    private readonly List<PaddingRule> _rules = new(4);

    // ----- Class tokens -----
    private const string _baseToken = "p";

    // ----- Size tokens -----
    private const string _token0 = "0";
    private const string _tokenAuto = "auto";

    // ----- Side tokens (Bootstrap naming) -----
    private const string _sideT = "t";
    private const string _sideE = "e";
    private const string _sideB = "b";
    private const string _sideS = "s";
    private const string _sideX = "x";
    private const string _sideY = "y";

    internal PaddingBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new PaddingRule(size, ElementSideType.All, breakpoint));
    }

    internal PaddingBuilder(List<PaddingRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Side chaining -----
    public PaddingBuilder FromTop => AddRule(ElementSideType.Top);
    public PaddingBuilder FromRight => AddRule(ElementSideType.Right);
    public PaddingBuilder FromBottom => AddRule(ElementSideType.Bottom);
    public PaddingBuilder FromLeft => AddRule(ElementSideType.Left);
    public PaddingBuilder OnX => AddRule(ElementSideType.Horizontal);
    public PaddingBuilder OnY => AddRule(ElementSideType.Vertical);
    public PaddingBuilder OnAll => AddRule(ElementSideType.All);
    public PaddingBuilder FromStart => AddRule(ElementSideType.InlineStart);
    public PaddingBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    // ----- Size chaining -----
    public PaddingBuilder Is0 => ChainWithSize(ScaleType.Is0);
    public PaddingBuilder Is1 => ChainWithSize(ScaleType.Is1);
    public PaddingBuilder Is2 => ChainWithSize(ScaleType.Is2);
    public PaddingBuilder Is3 => ChainWithSize(ScaleType.Is3);
    public PaddingBuilder Is4 => ChainWithSize(ScaleType.Is4);
    public PaddingBuilder Is5 => ChainWithSize(ScaleType.Is5);

    // ----- BreakpointType chaining -----
    public PaddingBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public PaddingBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public PaddingBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public PaddingBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public PaddingBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public PaddingBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder AddRule(ElementSideType side)
    {
        // Use last size & BreakpointType if present; default to ScaleType.Is0Value when absent
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        var bp = _rules.Count > 0 ? _rules[^1].breakpoint : null;

        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
        {
            // Replace last "All" with specific side using same size/bp
            _rules[^1] = new PaddingRule(size, side, bp);
        }
        else
        {
            _rules.Add(new PaddingRule(size, side, bp));
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder ChainWithSize(string size)
    {
        _rules.Add(new PaddingRule(size, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder ChainWithSize(ScaleType scale)
    {
        _rules.Add(new PaddingRule(scale.Value, ElementSideType.All, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new PaddingRule(ScaleType.Is0Value, ElementSideType.All, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new PaddingRule(last.Size, last.Side, breakpoint);
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

            var sideTok = GetSideToken(rule.Side); // "", "t", "e", "b", "s", "x", "y"
            var bpTok = BreakpointUtil.GetBreakpointToken(rule.breakpoint); // "", "sm", "md", ...

            if (!first) sb.Append(' ');
            else first = false;

            // Build: p{side?}-{bp?}-{size}
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
                        AppendStyle(ref first, ref sb, "padding", sizeVal);
                        break;

                    case ElementSideType.TopValue:
                        AppendStyle(ref first, ref sb, "padding-top", sizeVal);
                        break;

                    case ElementSideType.RightValue:
                        AppendStyle(ref first, ref sb, "padding-right", sizeVal);
                        break;

                    case ElementSideType.BottomValue:
                        AppendStyle(ref first, ref sb, "padding-bottom", sizeVal);
                        break;

                    case ElementSideType.LeftValue:
                        AppendStyle(ref first, ref sb, "padding-left", sizeVal);
                        break;

                    case ElementSideType.HorizontalValue:
                    case ElementSideType.LeftRightValue:
                        AppendStyle(ref first, ref sb, "padding-left", sizeVal);
                        AppendStyle(ref first, ref sb, "padding-right", sizeVal);
                        break;

                    case ElementSideType.VerticalValue:
                    case ElementSideType.TopBottomValue:
                        AppendStyle(ref first, ref sb, "padding-top", sizeVal);
                        AppendStyle(ref first, ref sb, "padding-bottom", sizeVal);
                        break;

                    case ElementSideType.InlineStartValue:
                        AppendStyle(ref first, ref sb, "padding-inline-start", sizeVal);
                        break;

                    case ElementSideType.InlineEndValue:
                        AppendStyle(ref first, ref sb, "padding-inline-end", sizeVal);
                        break;

                    default:
                        // Fallback like "all"
                        AppendStyle(ref first, ref sb, "padding", sizeVal);
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
            ScaleType.Is1Value => ScaleType.Is1Value,
            ScaleType.Is2Value => ScaleType.Is2Value,
            ScaleType.Is3Value => ScaleType.Is3Value,
            ScaleType.Is4Value => ScaleType.Is4Value,
            ScaleType.Is5Value => ScaleType.Is5Value,
            "-1" => _tokenAuto, // "auto"
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
                return _sideS; // Bootstrap uses "s" for start
            case ElementSideType.InlineEndValue:
                return _sideE; // Bootstrap uses "e" for end
            default:
                return string.Empty;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        // Match your original rem scale and "auto"
        return size switch
        {
            ScaleType.Is0Value => "0",
            ScaleType.Is1Value => "0.25rem",
            ScaleType.Is2Value => "0.5rem",
            ScaleType.Is3Value => "1rem",
            ScaleType.Is4Value => "1.5rem",
            ScaleType.Is5Value => "3rem",
            "-1" => "auto",
            _ => null
        };
    }

}

