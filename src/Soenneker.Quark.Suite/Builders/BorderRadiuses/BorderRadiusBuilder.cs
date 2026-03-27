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
    private BreakpointType? _pendingBreakpoint;

    // ----- Class tokens -----
    private const string _baseToken = "rounded";

    internal BorderRadiusBuilder(string size, BreakpointType? breakpoint = null, string cornerToken = "")
    {
        _cornerToken = cornerToken;
        if (size.HasContent())
            _rules.Add(new BorderRadiusRule(size, ElementSideType.All, breakpoint, cornerToken));
    }

    internal BorderRadiusBuilder(List<BorderRadiusRule> rules, BreakpointType? pendingBreakpoint = null)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
        _pendingBreakpoint = pendingBreakpoint;
    }

	/// <summary>
	/// Applies border radius to the top-left corner.
	/// </summary>
    public BorderRadiusBuilder TopLeft => new BorderRadiusBuilder(_rules, _pendingBreakpoint) { _cornerToken = "tl" };
	/// <summary>
	/// Applies border radius to the top-right corner.
	/// </summary>
    public BorderRadiusBuilder TopRight => new BorderRadiusBuilder(_rules, _pendingBreakpoint) { _cornerToken = "tr" };
	/// <summary>
	/// Applies border radius to the bottom-left corner.
	/// </summary>
    public BorderRadiusBuilder BottomLeft => new BorderRadiusBuilder(_rules, _pendingBreakpoint) { _cornerToken = "bl" };
	/// <summary>
	/// Applies border radius to the bottom-right corner.
	/// </summary>
    public BorderRadiusBuilder BottomRight => new BorderRadiusBuilder(_rules, _pendingBreakpoint) { _cornerToken = "br" };
	/// <summary>
	/// Applies border radius to the top side.
	/// </summary>
    public BorderRadiusBuilder Top => AddRule(ElementSideType.Top, "t");
	/// <summary>
	/// Applies border radius to the bottom side.
	/// </summary>
    public BorderRadiusBuilder Bottom => AddRule(ElementSideType.Bottom, "b");
	/// <summary>
	/// Applies border radius to the left side.
	/// </summary>
    public BorderRadiusBuilder Left => AddRule(ElementSideType.Left, "l");
	/// <summary>
	/// Applies border radius to the right side.
	/// </summary>
    public BorderRadiusBuilder Right => AddRule(ElementSideType.Right, "r");
	/// <summary>
	/// Applies border radius to all corners.
	/// </summary>
    public BorderRadiusBuilder All => AddRule(ElementSideType.All, "");

	/// <summary>
	/// Sets the border radius size to 0.
	/// </summary>
    public BorderRadiusBuilder Is0 => ChainWithSize(ScaleType.Is0);
	/// <summary>
	/// Sets the border radius to default size.
	/// </summary>
    public BorderRadiusBuilder Default => ChainWithSize("");
	/// <summary>
	/// Sets the border radius to pill shape.
	/// </summary>
    public BorderRadiusBuilder Pill => ChainWithSize("pill");
	/// <summary>
	/// Sets the border radius to circle shape.
	/// </summary>
    public BorderRadiusBuilder Circle => ChainWithSize("circle");
	/// <summary>
	/// Sets the border radius size to 1.
	/// </summary>
    public BorderRadiusBuilder Is1 => ChainWithSize("1");
	/// <summary>
	/// Sets the border radius size to 2.
	/// </summary>
    public BorderRadiusBuilder Is2 => ChainWithSize("2");
	/// <summary>
	/// Sets the border radius size to 3.
	/// </summary>
    public BorderRadiusBuilder Is3 => ChainWithSize("3");
	/// <summary>
	/// Sets the border radius size to 4.
	/// </summary>
    public BorderRadiusBuilder Is4 => ChainWithSize("4");
	/// <summary>
	/// Sets the border radius size to 5.
	/// </summary>
    public BorderRadiusBuilder Is5 => ChainWithSize("5");
	/// <summary>
	/// Sets the border radius size to Tailwind medium.
	/// </summary>
    public BorderRadiusBuilder Md => ChainWithSize("md");
	/// <summary>
	/// Sets the border radius size to Tailwind full.
	/// </summary>
    public BorderRadiusBuilder Full => ChainWithSize("full");

	/// <summary>
	/// Applies the border radius on phone breakpoint.
	/// </summary>
    public BorderRadiusBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the border radius on small breakpoint (≥640px).
	/// </summary>
    public BorderRadiusBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
	/// <summary>
	/// Applies the border radius on tablet breakpoint.
	/// </summary>
    public BorderRadiusBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the border radius on laptop breakpoint.
	/// </summary>
    public BorderRadiusBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the border radius on desktop breakpoint.
	/// </summary>
    public BorderRadiusBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the border radius on widescreen breakpoint.
	/// </summary>
    public BorderRadiusBuilder OnXxl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder AddRule(ElementSideType side, string cornerToken = "")
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : "0";
        var existingBp = _rules.Count > 0 ? _rules[^1].Breakpoint : null;
        var bp = _pendingBreakpoint ?? existingBp;
        _pendingBreakpoint = null;

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
            var bp = _pendingBreakpoint ?? _rules[^1].Breakpoint;
            _pendingBreakpoint = null;
            _rules[^1] = new BorderRadiusRule(size, _rules[^1].Side, bp, _rules[^1].CornerToken);
        }
        else
        {
            var bp = _pendingBreakpoint;
            _pendingBreakpoint = null;
            _rules.Add(new BorderRadiusRule(size, ElementSideType.All, bp, _cornerToken));
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder ChainWithSize(ScaleType scale)
    {
        // If the last rule has a corner token but no size, update it instead of adding new
        if (_rules.Count > 0 && _rules[^1].CornerToken.HasContent() && !_rules[^1].Size.HasContent())
        {
            var bp = _pendingBreakpoint ?? _rules[^1].Breakpoint;
            _pendingBreakpoint = null;
            _rules[^1] = new BorderRadiusRule(scale.Value, _rules[^1].Side, bp, _rules[^1].CornerToken);
        }
        else
        {
            var bp = _pendingBreakpoint;
            _pendingBreakpoint = null;
            _rules.Add(new BorderRadiusRule(scale.Value, ElementSideType.All, bp, _cornerToken));
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderRadiusBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
            var bpTok = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);

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
            "" => "var(--radius)",
            "pill" => "50rem",
            "circle" => "50%",
            "1" => "var(--radius-sm)",
            "2" => "var(--radius)",
            "3" => "var(--radius-lg)",
            "4" => "var(--radius-xl)",
            "5" => "calc(var(--radius-xl) + 4px)",
            _ => size
        };
    }
}
