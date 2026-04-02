using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Tailwind-aligned list layout variants.
/// </summary>
[TailwindPrefix("list-", Responsive = true)]
public sealed class ListVariantBuilder : ICssBuilder
{
    private readonly List<ListVariantRule> _rules = new(4);

    internal ListVariantBuilder(ListVariantType type, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ListVariantRule(type, breakpoint));
    }

    internal ListVariantBuilder(List<ListVariantRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Removes default list styling.
    /// </summary>
    public ListVariantBuilder None => Chain(ListVariantType.None);
    /// <summary>
    /// Sets the list variant to inline.
    /// </summary>
    public ListVariantBuilder Inline => Chain(ListVariantType.Inline);
    /// <summary>
    /// Sets the list variant to inline-item.
    /// </summary>
    public ListVariantBuilder InlineItem => Chain(ListVariantType.InlineItem);

    /// <summary>
    /// Applies the list variant on phone breakpoint.
    /// </summary>
    public ListVariantBuilder OnBase => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the list variant on small breakpoint (≥640px).
    /// </summary>
    public ListVariantBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the list variant on tablet breakpoint.
    /// </summary>
    public ListVariantBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the list variant on laptop breakpoint.
    /// </summary>
    public ListVariantBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the list variant on desktop breakpoint.
    /// </summary>
    public ListVariantBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the list variant on the 2xl breakpoint.
    /// </summary>
    public ListVariantBuilder On2xl => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ListVariantBuilder Chain(ListVariantType type)
    {
        _rules.Add(new ListVariantRule(type, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ListVariantBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ListVariantRule(ListVariantType.None, breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ListVariantRule(last.Type, breakpoint);
        return this;
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
            var typeValue = rule.Type.Value;
            var cls = typeValue switch
            {
                "none" => "list-none p-0",
                "inline" => "flex flex-wrap items-center gap-2 list-none p-0",
                "inline-item" => "inline-block",
                _ => string.Empty
            };

            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    /// <summary>
    /// Returns the CSS class string representation of this list variant builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}