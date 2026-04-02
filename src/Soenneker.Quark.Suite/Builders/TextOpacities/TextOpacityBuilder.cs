using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text opacity builder.
/// Produces Tailwind utility classes When possible, otherwise falls back to inline style.
/// </summary>
[TailwindPrefix("text-opacity-", Responsive = true)]
public sealed class TextOpacityBuilder : ICssBuilder
{
    private readonly List<TextOpacityRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal TextOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOpacityRule(value, breakpoint));
    }

    internal TextOpacityBuilder(List<TextOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the text opacity to 25 (25% opacity).
    /// </summary>
    public TextOpacityBuilder V25 => Chain(25);
    /// <summary>
    /// Sets the text opacity to 50 (50% opacity).
    /// </summary>
    public TextOpacityBuilder V50 => Chain(50);
    /// <summary>
    /// Sets the text opacity to 75 (75% opacity).
    /// </summary>
    public TextOpacityBuilder V75 => Chain(75);
    /// <summary>
    /// Sets the text opacity to 100 (fully opaque).
    /// </summary>
    public TextOpacityBuilder V100 => Chain(100);

    /// <summary>
    /// Applies the text opacity on phone breakpoint.
    /// </summary>
    public TextOpacityBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text opacity on small breakpoint (≥640px).
    /// </summary>
    public TextOpacityBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies the text opacity on tablet breakpoint.
    /// </summary>
    public TextOpacityBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text opacity on laptop breakpoint.
    /// </summary>
    public TextOpacityBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text opacity on desktop breakpoint.
    /// </summary>
    public TextOpacityBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text opacity on the 2xl breakpoint.
    /// </summary>
    public TextOpacityBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOpacityBuilder Chain(int value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new TextOpacityRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOpacityBuilder ChainBp(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
        return this;
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
            var cls = GetClass(rule.Value);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(int value)
    {
        return value switch
        {
            25 => "text-opacity-25",
            50 => "text-opacity-50",
            75 => "text-opacity-75",
            100 => "text-opacity-100",
            _ => string.Empty
        };
    }

    public override string ToString() => ToClass();
}