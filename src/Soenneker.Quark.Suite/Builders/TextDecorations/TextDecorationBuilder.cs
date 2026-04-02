using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text decoration builder with fluent API for chaining text decoration rules.
/// </summary>
[TailwindPrefix("decoration-", Responsive = true)]
public sealed class TextDecorationBuilder : ICssBuilder
{
    private readonly List<TextDecorationRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind text-decoration utilities (for Quark Suite / shadcn)
    private const string _classNone = "no-underline";
    private const string _classUnderline = "underline";
    private const string _classLineThrough = "line-through";
    private const string _classOverline = "overline";

    internal TextDecorationBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextDecorationRule(value, breakpoint));
    }

    internal TextDecorationBuilder(List<TextDecorationRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Line types
    /// <summary>
    /// Sets the text decoration to none (no decoration).
    /// </summary>
    public TextDecorationBuilder None => Chain(TextDecorationLineKeyword.NoneValue);
    /// <summary>
    /// Sets the text decoration to underline.
    /// </summary>
    public TextDecorationBuilder Underline => Chain(TextDecorationLineKeyword.UnderlineValue);
    /// <summary>
    /// Sets the text decoration to line-through.
    /// </summary>
    public TextDecorationBuilder LineThrough => Chain(TextDecorationLineKeyword.LineThroughValue);
    /// <summary>
    /// Sets the text decoration to overline.
    /// </summary>
    public TextDecorationBuilder Overline => Chain(TextDecorationLineKeyword.OverlineValue);

    // Breakpoints
    /// <summary>
    /// Applies the text decoration on phone breakpoint.
    /// </summary>
    public TextDecorationBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the text decoration on small breakpoint (≥640px).
    /// </summary>
    public TextDecorationBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the text decoration on tablet breakpoint.
    /// </summary>
    public TextDecorationBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the text decoration on laptop breakpoint.
    /// </summary>
    public TextDecorationBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the text decoration on desktop breakpoint.
    /// </summary>
    public TextDecorationBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the text decoration on the 2xl breakpoint.
    /// </summary>
    public TextDecorationBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder Chain(string value)
    {
        _rules.Add(new TextDecorationRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextDecorationBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];

            var baseClass = rule.Value switch
            {
                TextDecorationLineKeyword.NoneValue => _classNone,
                TextDecorationLineKeyword.UnderlineValue => _classUnderline,
                TextDecorationLineKeyword.LineThroughValue => _classLineThrough,
                TextDecorationLineKeyword.OverlineValue => _classOverline,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
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