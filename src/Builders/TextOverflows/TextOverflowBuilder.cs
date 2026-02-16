using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

using TextOverflowEnum = Soenneker.Quark.Enums.TextOverflowKeyword;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text-overflow builder with fluent API for chaining rules.
/// </summary>
public sealed class TextOverflowBuilder : ICssBuilder
{
    private readonly List<TextOverflowRule> _rules = new(4);

    // ----- Class constants -----
    private const string _classTruncate = "text-truncate";

    // ----- CSS prefix -----
    private const string _textOverflowPrefix = "text-overflow: ";

    internal TextOverflowBuilder(TextOverflowEnum textOverflow, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOverflowRule(textOverflow.Value, breakpoint));
    }

    internal TextOverflowBuilder(string textOverflow, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOverflowRule(textOverflow, breakpoint));
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

    // ----- Fluent chaining (Global keywords) -----
    /// <summary>
    /// Sets the text overflow to inherit.
    /// </summary>
    public TextOverflowBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the text overflow to initial.
    /// </summary>
    public TextOverflowBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the text overflow to revert.
    /// </summary>
    public TextOverflowBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the text overflow to revert-layer.
    /// </summary>
    public TextOverflowBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the text overflow to unset.
    /// </summary>
    public TextOverflowBuilder Unset => Chain(GlobalKeyword.UnsetValue);

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
        _rules.Add(new TextOverflowRule(value.Value, null));
        return this;
    }

    // Overload for global keywords (assumes a ctor: TextOverflowRule(GlobalKeyword, BreakpointType?))
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(GlobalKeyword keyword)
    {
        _rules.Add(new TextOverflowRule(keyword.Value, null));
        return this;
    }

    // Overload for string values
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder Chain(string value)
    {
        _rules.Add(new TextOverflowRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with Clip if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOverflowBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextOverflowRule(TextOverflowEnum.Clip, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextOverflowRule last = _rules[lastIdx];

        // Re-create the rule preserving its text-overflow value using the string value.
        _rules[lastIdx] = new TextOverflowRule(last.Value, bp);
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
            TextOverflowRule rule = _rules[i];

            // Only Clip/Ellipsis map to a Bootstrap class; keywords don't.
            string baseClass = GetTextOverflowClass(rule.Value);
            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.InsertBreakpointType(baseClass, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            TextOverflowRule rule = _rules[i];

            // Always use the string value (covers Clip/Ellipsis and any keyword values)
            string value = rule.Value;

            if (string.IsNullOrEmpty(value))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_textOverflowPrefix);
            sb.Append(value);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetTextOverflowClass(string textOverflow)
    {
        switch (textOverflow)
        {
            case "clip":
            case "ellipsis":
                return _classTruncate;
            default:
                return string.Empty;
        }
    }

}
