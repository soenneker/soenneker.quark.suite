using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Tailwind font variant numeric utility builder.
/// </summary>
[TailwindPrefix("normal-nums", Responsive = true)]
public sealed class FontVariantNumericBuilder : ICssBuilder
{
    private readonly List<FontVariantNumericRule> _rules = new(6);
    private BreakpointType? _pendingBreakpoint;

    internal FontVariantNumericBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FontVariantNumericRule(value, breakpoint));
    }

    internal FontVariantNumericBuilder(List<FontVariantNumericRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Normal Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder NormalNums => Chain("normal-nums");
    /// <summary>
    /// Fluent step for `Ordinal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder Ordinal => Chain("ordinal");
    /// <summary>
    /// Fluent step for `Slashed Zero` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder SlashedZero => Chain("slashed-zero");
    /// <summary>
    /// Fluent step for `Lining Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder LiningNums => Chain("lining-nums");
    /// <summary>
    /// Fluent step for `Oldstyle Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder OldstyleNums => Chain("oldstyle-nums");
    /// <summary>
    /// Fluent step for `Proportional Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder ProportionalNums => Chain("proportional-nums");
    /// <summary>
    /// Fluent step for `Tabular Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder TabularNums => Chain("tabular-nums");
    /// <summary>
    /// Fluent step for `Diagonal Fractions` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder DiagonalFractions => Chain("diagonal-fractions");
    /// <summary>
    /// Fluent step for `Stacked Fractions` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public FontVariantNumericBuilder StackedFractions => Chain("stacked-fractions");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public FontVariantNumericBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public FontVariantNumericBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public FontVariantNumericBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public FontVariantNumericBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public FontVariantNumericBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public FontVariantNumericBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontVariantNumericBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new FontVariantNumericRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontVariantNumericBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetClass(rule.Value);

            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var css = GetStyleValue(_rules[i].Value);

            if (css is null)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append("font-variant-numeric: ");
            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(string value)
    {
        return value switch
        {
            "normal-nums" => "normal-nums",
            "ordinal" => "ordinal",
            "slashed-zero" => "slashed-zero",
            "lining-nums" => "lining-nums",
            "oldstyle-nums" => "oldstyle-nums",
            "proportional-nums" => "proportional-nums",
            "tabular-nums" => "tabular-nums",
            "diagonal-fractions" => "diagonal-fractions",
            "stacked-fractions" => "stacked-fractions",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyleValue(string value)
    {
        return value switch
        {
            "normal-nums" => "normal",
            "ordinal" => "ordinal",
            "slashed-zero" => "slashed-zero",
            "lining-nums" => "lining-nums",
            "oldstyle-nums" => "oldstyle-nums",
            "proportional-nums" => "proportional-nums",
            "tabular-nums" => "tabular-nums",
            "diagonal-fractions" => "diagonal-fractions",
            "stacked-fractions" => "stacked-fractions",
            _ => null
        };
    }
}
