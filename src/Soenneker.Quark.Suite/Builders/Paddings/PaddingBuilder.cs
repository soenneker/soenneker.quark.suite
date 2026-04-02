using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance padding builder with fluent API for chaining padding rules.
/// </summary>
[TailwindPrefix("p-", Responsive = true)]
public sealed class PaddingBuilder : ICssBuilder
{
    private readonly List<PaddingRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // ----- Class tokens -----
    private const string _baseToken = "p";

    // ----- Size tokens -----
    private const string _token0 = "0";
    private const string _token6 = "6";
    private const string _token8 = "8";
    private const string _token16 = "16";
    private const string _tokenAuto = "auto";

    // ----- Side tokens (Tailwind: t=top, e=end, b=bottom, s=start, x=horizontal, y=vertical) -----
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

	/// <summary>
	/// Applies padding from the top side.
	/// </summary>
    public PaddingBuilder FromTop => AddRule(ElementSideType.Top);
	/// <summary>
	/// Applies padding from the right side.
	/// </summary>
    public PaddingBuilder FromRight => AddRule(ElementSideType.Right);
	/// <summary>
	/// Applies padding from the bottom side.
	/// </summary>
    public PaddingBuilder FromBottom => AddRule(ElementSideType.Bottom);
	/// <summary>
	/// Applies padding from the left side.
	/// </summary>
    public PaddingBuilder FromLeft => AddRule(ElementSideType.Left);
	/// <summary>
	/// Applies padding on the horizontal axis (left and right).
	/// </summary>
    public PaddingBuilder OnX => AddRule(ElementSideType.Horizontal);
	/// <summary>
	/// Applies padding on the vertical axis (top and bottom).
	/// </summary>
    public PaddingBuilder OnY => AddRule(ElementSideType.Vertical);
	/// <summary>
	/// Applies padding on all sides.
	/// </summary>
    public PaddingBuilder OnAll => AddRule(ElementSideType.All);
	/// <summary>
	/// Applies padding from the inline start.
	/// </summary>
    public PaddingBuilder FromStart => AddRule(ElementSideType.InlineStart);
	/// <summary>
	/// Applies padding from the inline end.
	/// </summary>
    public PaddingBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

	/// <summary>
	/// Sets the padding size from an arbitrary Tailwind spacing token.
	/// </summary>
    public PaddingBuilder Is0 => ChainWithSize(ScaleType.Is0);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is1 => ChainWithSize(ScaleType.Is1);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is2 => ChainWithSize(ScaleType.Is2);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is3 => ChainWithSize(ScaleType.Is3);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is4 => ChainWithSize(ScaleType.Is4);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is5 => ChainWithSize(ScaleType.Is5);
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is6 => ChainWithSize("6");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is8 => ChainWithSize("8");
    /// <summary>
    /// Spacing/sizing scale step `16` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 16` for integer spacing utilities unless overridden).
    /// </summary>
    public PaddingBuilder Is16 => ChainWithSize("16");

    /// <summary>
    /// Tailwind token segment (spacing scale step, arbitrary value like `[17rem]`, or theme key). Builds the matching utility class for this builder.
    /// </summary>
    /// <param name="value">Suffix/token after the utility prefix (see Tailwind docs for this family).</param>
    public PaddingBuilder Token(string value) => ChainWithSize(value);

	/// <summary>
	/// Applies the padding on phone breakpoint.
	/// </summary>
    public PaddingBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the padding on small breakpoint (≥640px).
	/// </summary>
    public PaddingBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
	/// <summary>
	/// Applies the padding on tablet breakpoint.
	/// </summary>
    public PaddingBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the padding on laptop breakpoint.
	/// </summary>
    public PaddingBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the padding on desktop breakpoint.
	/// </summary>
    public PaddingBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the padding on the 2xl breakpoint.
	/// </summary>
    public PaddingBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder AddRule(ElementSideType side)
    {
        // Use last size & BreakpointType if present; default to ScaleType.Is0Value When absent
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        var existingBp = _rules.Count > 0 ? _rules[^1].Breakpoint : null;
        var bp = _pendingBreakpoint ?? existingBp;
        _pendingBreakpoint = null;

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
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new PaddingRule(size, ElementSideType.All, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder ChainWithSize(ScaleType scale)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new PaddingRule(scale.Value, ElementSideType.All, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PaddingBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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

            var sizeTok = GetSizeToken(rule.Size);
            if (sizeTok.Length == 0)
                continue;

            var sideTok = GetSideToken(rule.Side); // "", "t", "e", "b", "s", "x", "y"
            var bpTok = BreakpointUtil.GetBreakpointToken(rule.Breakpoint); // "", "sm", "md", ...

            if (!first) sb.Append(' ');
            else first = false;

            // Tailwind-style: p-4, md:p-4 (breakpoint-prefixed)
            var baseClass = _baseToken + (sideTok.Length != 0 ? sideTok : "") + "-" + sizeTok;
            var cls = bpTok.Length != 0 ? BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bpTok) : baseClass;
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
            "6" => _token6,
            "8" => _token8,
            "16" => _token16,
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
                return _sideS; // inline-start
            case ElementSideType.InlineEndValue:
                return _sideE; // inline-end
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
            "6" => "1.5rem",
            "8" => "2rem",
            "16" => "4rem",
            "-1" => "auto",
            _ => size
        };
    }

}