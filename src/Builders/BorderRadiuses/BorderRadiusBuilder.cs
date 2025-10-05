using Soenneker.Quark.Enums;
using Soenneker.Extensions.String;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    internal BorderRadiusBuilder(string size, BreakpointType? breakpoint = null, string cornerToken = "")
    {
        _cornerToken = cornerToken;
        if (size.HasContent())
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
    public BorderRadiusBuilder Is0 => ChainWithSize(ScaleType.Is0);
    public BorderRadiusBuilder Small => ChainWithSize("sm");
    public BorderRadiusBuilder Default => ChainWithSize("");
    public BorderRadiusBuilder Large => ChainWithSize("lg");
    public BorderRadiusBuilder ExtraLarge => ChainWithSize("xl");
    public BorderRadiusBuilder Xxl => ChainWithSize("2xl");
    public BorderRadiusBuilder Pill => ChainWithSize("pill");
    public BorderRadiusBuilder Circle => ChainWithSize("circle");


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder AddRule(ElementSideType side, string cornerToken = "")
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : "0";
        var bp = _rules.Count > 0 ? _rules[^1].breakpoint : null;

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
        if (_rules.Count > 0 && _rules[^1].CornerToken.HasContent() && !_rules[^1].Size.HasContent())
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
        if (_rules.Count > 0 && _rules[^1].CornerToken.HasContent() && !_rules[^1].Size.HasContent())
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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
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
            var rule = _rules[i];

            var sizeTok = rule.Size;

            if (sizeTok.Length == 0)
                continue;

            var cornerTok = rule.CornerToken;
            var bpTok = BreakpointUtil.GetBreakpointToken(rule.breakpoint);

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
                var rule = _rules[i];
                var sizeVal = GetSizeValue(rule.Size);

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
            ScaleType.Is0Value => "0",
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
