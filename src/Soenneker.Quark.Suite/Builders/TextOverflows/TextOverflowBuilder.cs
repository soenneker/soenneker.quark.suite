using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

using TextOverflowEnum = Soenneker.Quark.TextOverflowKeyword;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text-overflow builder with fluent API for chaining rules.
/// </summary>
[TailwindPrefix("text-", Responsive = true)]
public sealed class TextOverflowBuilder : ICssBuilder
{
    private readonly List<TextOverflowRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind text-overflow utilities.
    private const string _classEllipsis = "text-ellipsis";
    private const string _classClip = "text-clip";

    // ----- CSS prefix -----
    private const string _textOverflowPrefix = "text-overflow: ";

    internal TextOverflowBuilder(TextOverflowEnum textOverflow, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOverflowRule(textOverflow.Value, breakpoint));
    }

    internal TextOverflowBuilder(List<TextOverflowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // ----- Fluent chaining (TextOverflow enum) -----
    /// <summary>
    /// Sets the text overflow to clip.
    /// </summary>
    public TextOverflowBuilder Clip => Chain(TextOverflowEnum.Clip);
    /// <summary>
    /// Sets the text overflow to ellipsis.
    /// </summary>
    public TextOverflowBuilder Ellipsis => Chain(TextOverflowEnum.Ellipsis);

    // ----- BreakpointType chaining -----
    /// <summary>
    /// Applies the text overflow at base (mobile-first default).
    /// </summary>
    public TextOverflowBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text overflow at sm breakpoint (≥640px).
    /// </summary>
    public TextOverflowBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies the text overflow at md breakpoint (≥768px).
    /// </summary>
    public TextOverflowBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text overflow at lg breakpoint (≥1024px).
    /// </summary>
    public TextOverflowBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text overflow at xl breakpoint (≥1280px).
    /// </summary>
    public TextOverflowBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text overflow at 2xl breakpoint (≥1536px).
    /// </summary>
    public TextOverflowBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(TextOverflowEnum value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new TextOverflowRule(value.Value, breakpoint));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or seed with Clip if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder ChainBp(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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

            // Only Clip/Ellipsis map to utility classes; keywords don't.
            var baseClass = GetTextOverflowClass(rule.Value);
            if (baseClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.ApplyTailwindBreakpoint(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle()
    {
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetTextOverflowClass(string textOverflow)
    {
        return textOverflow switch
        {
            "clip" => _classClip,
            "ellipsis" => _classEllipsis,
            _ => string.Empty
        };
    }

    public override string ToString() => ToClass();
}
