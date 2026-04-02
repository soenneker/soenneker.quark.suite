using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text transform builder with fluent API for chaining text transform rules.
/// </summary>
[TailwindPrefix("uppercase", Responsive = true)]
public sealed class TextTransformBuilder : ICssBuilder
{
    private readonly List<TextTransformRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind text-transform utilities (for Quark Suite / shadcn)
    private const string _classLower = "lowercase";
    private const string _classUpper = "uppercase";
    private const string _classCap = "capitalize";
    internal TextTransformBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextTransformRule(value, breakpoint));
    }

    internal TextTransformBuilder(List<TextTransformRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the text transform to lowercase.
    /// </summary>
    public TextTransformBuilder Lowercase => Chain(TextTransformKeyword.LowercaseValue);
    /// <summary>
    /// Sets the text transform to uppercase.
    /// </summary>
    public TextTransformBuilder Uppercase => Chain(TextTransformKeyword.UppercaseValue);
    /// <summary>
    /// Sets the text transform to capitalize.
    /// </summary>
    public TextTransformBuilder Capitalize => Chain(TextTransformKeyword.CapitalizeValue);
    /// <summary>
    /// Applies the text transform on phone breakpoint.
    /// </summary>
    public TextTransformBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the text transform on small breakpoint (≥640px).
    /// </summary>
    public TextTransformBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the text transform on tablet breakpoint.
    /// </summary>
    public TextTransformBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the text transform on laptop breakpoint.
    /// </summary>
    public TextTransformBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the text transform on desktop breakpoint.
    /// </summary>
    public TextTransformBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the text transform on the 2xl breakpoint.
    /// </summary>
    public TextTransformBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder Chain(string value)
    {
        _rules.Add(new TextTransformRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                TextTransformKeyword.LowercaseValue => _classLower,
                TextTransformKeyword.UppercaseValue => _classUpper,
                TextTransformKeyword.CapitalizeValue => _classCap,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        return string.Empty;
    }

    public override string ToString() => ToClass();
}