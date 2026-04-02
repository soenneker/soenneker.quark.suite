using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll margin builder. Tailwind: scroll-m-*, scroll-mt-*, scroll-mr-*, etc.
/// </summary>
[TailwindPrefix("scroll-m", Responsive = true)]
public sealed class ScrollMarginBuilder : ICssBuilder
{
    private readonly List<ScrollMarginRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    private const string _baseToken = "scroll-m";
    private const string _sideT = "t";
    private const string _sideE = "e";
    private const string _sideB = "b";
    private const string _sideS = "s";
    private const string _sideX = "x";
    private const string _sideY = "y";

    internal ScrollMarginBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollMarginRule(size, ElementSideType.All, breakpoint));
    }

    internal ScrollMarginBuilder(List<ScrollMarginRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `From Top` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromTop => AddRule(ElementSideType.Top);
    /// <summary>
    /// Fluent step for `From Right` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromRight => AddRule(ElementSideType.Right);
    /// <summary>
    /// Fluent step for `From Bottom` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromBottom => AddRule(ElementSideType.Bottom);
    /// <summary>
    /// Fluent step for `From Left` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromLeft => AddRule(ElementSideType.Left);
    /// <summary>
    /// Fluent step for `On X` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder OnX => AddRule(ElementSideType.Horizontal);
    /// <summary>
    /// Fluent step for `On Y` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder OnY => AddRule(ElementSideType.Vertical);
    /// <summary>
    /// Fluent step for `On All` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder OnAll => AddRule(ElementSideType.All);
    /// <summary>
    /// Fluent step for `From Start` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromStart => AddRule(ElementSideType.InlineStart);
    /// <summary>
    /// Fluent step for `From End` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollMarginBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is0 => ChainWithSize(ScaleType.Is0);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is1 => ChainWithSize(ScaleType.Is1);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is2 => ChainWithSize(ScaleType.Is2);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is3 => ChainWithSize(ScaleType.Is3);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is4 => ChainWithSize(ScaleType.Is4);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is5 => ChainWithSize(ScaleType.Is5);
    /// <summary>
    /// Spacing/sizing scale step `24` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 24` for integer spacing utilities unless overridden).
    /// </summary>
    public ScrollMarginBuilder Is24 => ChainWithSize("24");
    /// <summary>
    /// One pixel (`px` unit) — hairline borders, fixed 1px tracks, etc.
    /// </summary>
    public ScrollMarginBuilder Px => ChainWithSize("px");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ScrollMarginBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ScrollMarginBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ScrollMarginBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ScrollMarginBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public ScrollMarginBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollMarginBuilder AddRule(ElementSideType side)
    {
        var size = _rules.Count > 0 ? _rules[^1].Size : ScaleType.Is0Value;
        var existingBp = _rules.Count > 0 ? _rules[^1].Breakpoint : null;
        var bp = _pendingBreakpoint ?? existingBp;
        _pendingBreakpoint = null;
        if (_rules.Count > 0 && _rules[^1].Side == ElementSideType.All)
            _rules[^1] = new ScrollMarginRule(size, side, bp);
        else
            _rules.Add(new ScrollMarginRule(size, side, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollMarginBuilder ChainWithSize(string size)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ScrollMarginRule(size, ElementSideType.All, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollMarginBuilder ChainWithSize(ScaleType scale)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ScrollMarginRule(scale.Value, ElementSideType.All, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollMarginBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
                case ElementSideType.AllValue: prop = "scroll-margin"; break;
                case ElementSideType.TopValue: prop = "scroll-margin-top"; break;
                case ElementSideType.RightValue: prop = "scroll-margin-right"; break;
                case ElementSideType.BottomValue: prop = "scroll-margin-bottom"; break;
                case ElementSideType.LeftValue: prop = "scroll-margin-left"; break;
                case ElementSideType.HorizontalValue:
                case ElementSideType.LeftRightValue: prop = "scroll-margin-inline"; break;
                case ElementSideType.VerticalValue:
                case ElementSideType.TopBottomValue: prop = "scroll-margin-block"; break;
                case ElementSideType.InlineStartValue: prop = "scroll-margin-inline-start"; break;
                case ElementSideType.InlineEndValue: prop = "scroll-margin-inline-end"; break;
                default: prop = "scroll-margin"; break;
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
        "24" => "24",
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
        "24" => "6rem",
        "px" => "1px",
        _ => null
    };
}
