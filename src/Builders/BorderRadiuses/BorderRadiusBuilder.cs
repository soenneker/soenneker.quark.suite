using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified border radius builder with fluent API for chaining border radius rules.
/// </summary>
public sealed class BorderRadiusBuilder : ICssBuilder
{
    private readonly List<BorderRadiusRule> _rules = new(4);
    private string _cornerToken = "";

    // ----- Class tokens -----
    private const string _baseToken = "rounded";

    // ----- Corner tokens -----
    private const string _cornerTl = "tl"; // top-left
    private const string _cornerTr = "tr"; // top-right
    private const string _cornerBl = "bl"; // bottom-left
    private const string _cornerBr = "br"; // bottom-right
    private const string _cornerT = "t";   // top (both corners)
    private const string _cornerB = "b";  // bottom (both corners)
    private const string _cornerL = "l";  // left (both corners)
    private const string _cornerR = "r";  // right (both corners)

    internal BorderRadiusBuilder(string size, BreakpointType? breakpoint = null, string cornerToken = "")
    {
        _cornerToken = cornerToken;
        if (!string.IsNullOrEmpty(size))
            _rules.Add(new BorderRadiusRule(size, ElementSideType.All, breakpoint, cornerToken));
    }

    internal BorderRadiusBuilder(List<BorderRadiusRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Corner chaining -----
    public BorderRadiusBuilder TopLeft => new(_rules) { _cornerToken = "tl" };
    public BorderRadiusBuilder TopRight => new(_rules) { _cornerToken = "tr" };
    public BorderRadiusBuilder BottomLeft => new(_rules) { _cornerToken = "bl" };
    public BorderRadiusBuilder BottomRight => new(_rules) { _cornerToken = "br" };
    public BorderRadiusBuilder Top => AddRule(ElementSideType.Top, "t");
    public BorderRadiusBuilder Bottom => AddRule(ElementSideType.Bottom, "b");
    public BorderRadiusBuilder Left => AddRule(ElementSideType.Left, "l");
    public BorderRadiusBuilder Right => AddRule(ElementSideType.Right, "r");
    public BorderRadiusBuilder All => AddRule(ElementSideType.All, "");

    // ----- Size chaining -----
    public BorderRadiusBuilder S0 => ChainWithSize(ScaleType.S0);
    public BorderRadiusBuilder Small => ChainWithSize("sm");
    public BorderRadiusBuilder Default => ChainWithSize("");
    public BorderRadiusBuilder Large => ChainWithSize("lg");
    public BorderRadiusBuilder ExtraLarge => ChainWithSize("xl");
    public BorderRadiusBuilder Xxl => ChainWithSize("2xl");
    public BorderRadiusBuilder Pill => ChainWithSize("pill");
    public BorderRadiusBuilder Circle => ChainWithSize("circle");

    // ----- BreakpointType chaining -----
    public BorderRadiusBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public BorderRadiusBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public BorderRadiusBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public BorderRadiusBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public BorderRadiusBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public BorderRadiusBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder AddRule(ElementSideType side, string cornerToken = "")
    {
        string size = _rules.Count > 0 ? _rules[^1].Size : "0";
        BreakpointType? bp = _rules.Count > 0 ? _rules[^1].breakpoint : null;

        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
        {
            _rules[^1] = new BorderRadiusRule(size, side, bp, cornerToken);
        }
        else
        {
            _rules.Add(new BorderRadiusRule(size, side, bp, cornerToken));
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder ChainWithSize(string size)
    {
        // If the last rule has a corner token but no size, update it instead of adding new
        if (_rules.Count > 0 && !string.IsNullOrEmpty(_rules[^1].CornerToken) && string.IsNullOrEmpty(_rules[^1].Size))
        {
            _rules[^1] = new BorderRadiusRule(size, _rules[^1].Side, _rules[^1].breakpoint, _rules[^1].CornerToken);
        }
        else
        {
            _rules.Add(new BorderRadiusRule(size, ElementSideType.All, null, _cornerToken));
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder ChainWithSize(ScaleType scale)
    {
        // If the last rule has a corner token but no size, update it instead of adding new
        if (_rules.Count > 0 && !string.IsNullOrEmpty(_rules[^1].CornerToken) && string.IsNullOrEmpty(_rules[^1].Size))
        {
            _rules[^1] = new BorderRadiusRule(scale.Value, _rules[^1].Side, _rules[^1].breakpoint, _rules[^1].CornerToken);
        }
        else
        {
            _rules.Add(new BorderRadiusRule(scale.Value, ElementSideType.All, null, _cornerToken));
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BorderRadiusRule("0", ElementSideType.All, breakpoint, ""));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        BorderRadiusRule last = _rules[lastIdx];
        _rules[lastIdx] = new BorderRadiusRule(last.Size, last.Side, breakpoint, last.CornerToken);
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
            BorderRadiusRule rule = _rules[i];

            string sizeTok = rule.Size;

            if (sizeTok.Length == 0)
                continue;

            string cornerTok = rule.CornerToken;
            string bpTok = BreakpointUtil.GetBreakpointToken(rule.breakpoint);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(_baseToken);

            if (cornerTok.Length != 0)
            {
                sb.Append('-');
                sb.Append(cornerTok);
            }

            if (sizeTok != "0" && sizeTok.Length > 0)
            {
                sb.Append('-');
                sb.Append(sizeTok);
            }

            if (bpTok.Length != 0)
            {
                sb.Append('-');
                sb.Append(bpTok);
            }
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
                BorderRadiusRule rule = _rules[i];
                string? sizeVal = GetSizeValue(rule.Size);

                if (sizeVal is null)
                    continue;

                // Use corner token to determine which CSS properties to set
                switch (rule.CornerToken)
                {
                    case "tl":
                        AppendStyle(ref first, ref sb, "border-top-left-radius", sizeVal);
                        break;
                    case "tr":
                        AppendStyle(ref first, ref sb, "border-top-right-radius", sizeVal);
                        break;
                    case "bl":
                        AppendStyle(ref first, ref sb, "border-bottom-left-radius", sizeVal);
                        break;
                    case "br":
                        AppendStyle(ref first, ref sb, "border-bottom-right-radius", sizeVal);
                        break;
                    case "t":
                        AppendStyle(ref first, ref sb, "border-top-left-radius", sizeVal);
                        AppendStyle(ref first, ref sb, "border-top-right-radius", sizeVal);
                        break;
                    case "b":
                        AppendStyle(ref first, ref sb, "border-bottom-left-radius", sizeVal);
                        AppendStyle(ref first, ref sb, "border-bottom-right-radius", sizeVal);
                        break;
                    case "l":
                        AppendStyle(ref first, ref sb, "border-top-left-radius", sizeVal);
                        AppendStyle(ref first, ref sb, "border-bottom-left-radius", sizeVal);
                        break;
                    case "r":
                        AppendStyle(ref first, ref sb, "border-top-right-radius", sizeVal);
                        AppendStyle(ref first, ref sb, "border-bottom-right-radius", sizeVal);
                        break;
                    default:
                        AppendStyle(ref first, ref sb, "border-radius", sizeVal);
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

    /// <summary>Gets the string representation of the current configuration.</summary>
    public override string ToString()
    {
        return ToClass();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        return size switch
        {
            ScaleType.S0Value => "0",
            "sm" => "0.25rem",
            "" => "0.375rem",
            "lg" => "0.5rem",
            "xl" => "1rem",
            "2xl" => "2rem",
            "pill" => "50rem",
            "circle" => "50%",
            _ => null
        };
    }
}
