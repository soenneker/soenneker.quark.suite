using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
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

    public FontVariantNumericBuilder NormalNums => Chain("normal-nums");
    public FontVariantNumericBuilder Ordinal => Chain("ordinal");
    public FontVariantNumericBuilder SlashedZero => Chain("slashed-zero");
    public FontVariantNumericBuilder LiningNums => Chain("lining-nums");
    public FontVariantNumericBuilder OldstyleNums => Chain("oldstyle-nums");
    public FontVariantNumericBuilder ProportionalNums => Chain("proportional-nums");
    public FontVariantNumericBuilder TabularNums => Chain("tabular-nums");
    public FontVariantNumericBuilder DiagonalFractions => Chain("diagonal-fractions");
    public FontVariantNumericBuilder StackedFractions => Chain("stacked-fractions");

    public FontVariantNumericBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public FontVariantNumericBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public FontVariantNumericBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public FontVariantNumericBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public FontVariantNumericBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
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
