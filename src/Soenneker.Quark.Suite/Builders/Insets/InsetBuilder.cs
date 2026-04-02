using Soenneker.Quark.Attributes;
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

    /// <summary>
    /// Fluent step for `From Top` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromTop => AddRule(ElementSideType.Top);
    /// <summary>
    /// Fluent step for `From Right` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromRight => AddRule(ElementSideType.Right);
    /// <summary>
    /// Fluent step for `From Bottom` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromBottom => AddRule(ElementSideType.Bottom);
    /// <summary>
    /// Fluent step for `From Left` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromLeft => AddRule(ElementSideType.Left);
    /// <summary>
    /// Fluent step for `On X` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder OnX => AddRule(ElementSideType.Horizontal);
    /// <summary>
    /// Fluent step for `On Y` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder OnY => AddRule(ElementSideType.Vertical);
    /// <summary>
    /// Fluent step for `On All` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder OnAll => AddRule(ElementSideType.All);
    /// <summary>
    /// Fluent step for `From Start` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromStart => AddRule(ElementSideType.InlineStart);
    /// <summary>
    /// Fluent step for `From End` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public InsetBuilder FromEnd => AddRule(ElementSideType.InlineEnd);

    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is0 => ChainWithSize(ScaleType.Is0);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is1 => ChainWithSize(ScaleType.Is1);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is2 => ChainWithSize(ScaleType.Is2);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is3 => ChainWithSize(ScaleType.Is3);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is4 => ChainWithSize(ScaleType.Is4);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public InsetBuilder Is5 => ChainWithSize(ScaleType.Is5);
    /// <summary>
    /// One pixel (`px` unit) — hairline borders, fixed 1px tracks, etc.
    /// </summary>
    public InsetBuilder Px => ChainWithSize(_tokenPx);
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public InsetBuilder Auto => ChainWithSize("auto");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public InsetBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public InsetBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public InsetBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public InsetBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public InsetBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

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
