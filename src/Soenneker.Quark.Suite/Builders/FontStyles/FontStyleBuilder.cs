using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance font style builder with fluent API for chaining font style rules.
/// </summary>
[TailwindPrefix("italic", Responsive = true)]
public sealed class FontStyleBuilder : ICssBuilder
{
    private readonly List<FontStyleRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind font-style utilities (for Quark Suite / shadcn)
    private const string _classItalic = "italic";
    private const string _classNormal = "not-italic";
    internal FontStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FontStyleRule(value, breakpoint));
    }

    internal FontStyleBuilder(List<FontStyleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the font style to italic.
    /// </summary>
    public FontStyleBuilder Italic => Chain(FontStyleKeyword.ItalicValue);
    /// <summary>
    /// Sets the font style to normal.
    /// </summary>
    public FontStyleBuilder Normal => Chain(FontStyleKeyword.NormalValue);
    /// <summary>
    /// Applies the font style on phone breakpoint.
    /// </summary>
    public FontStyleBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the font style on small breakpoint (≥640px).
    /// </summary>
    public FontStyleBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the font style on tablet breakpoint.
    /// </summary>
    public FontStyleBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the font style on laptop breakpoint.
    /// </summary>
    public FontStyleBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the font style on desktop breakpoint.
    /// </summary>
    public FontStyleBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the font style on the 2xl breakpoint.
    /// </summary>
    public FontStyleBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontStyleBuilder Chain(string value)
    {
        _rules.Add(new FontStyleRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontStyleBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
                FontStyleKeyword.ItalicValue => _classItalic,
                FontStyleKeyword.NormalValue => _classNormal,
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