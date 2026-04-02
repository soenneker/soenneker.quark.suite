using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned text size builder.
/// </summary>
[TailwindPrefix("text-", Responsive = true)]
public sealed class TextSizeBuilder : ICssBuilder
{
    private readonly List<TextSizeRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal TextSizeBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextSizeRule(size, breakpoint));
    }

    internal TextSizeBuilder(List<TextSizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextSizeBuilder Xs => ChainSize("xs");
    public TextSizeBuilder Sm => ChainSize("sm");
    public TextSizeBuilder Base => ChainSize("base");
    public TextSizeBuilder Lg => ChainSize("lg");
    public TextSizeBuilder Xl => ChainSize("xl");
    public TextSizeBuilder TwoXl => ChainSize("2xl");
    public TextSizeBuilder ThreeXl => ChainSize("3xl");
    public TextSizeBuilder FourXl => ChainSize("4xl");

    public TextSizeBuilder Token(string value) => ChainSize(value);

    // ----- BreakpointType chaining -----
    /// <summary>
    /// Applies the text size on phone breakpoint.
    /// </summary>
    public TextSizeBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the text size on small breakpoint (≥640px).
    /// </summary>
    public TextSizeBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the text size on tablet breakpoint.
    /// </summary>
    public TextSizeBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the text size on laptop breakpoint.
    /// </summary>
    public TextSizeBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the text size on desktop breakpoint.
    /// </summary>
    public TextSizeBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public TextSizeBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder ChainSize(string size)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new TextSizeRule(size, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextSizeBuilder SetPendingBreakpoint(BreakpointType bp)
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

            var sizeClass = GetSizeClass(rule.Size);
            if (sizeClass.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                sizeClass = BreakpointUtil.ApplyTailwindBreakpoint(sizeClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(sizeClass);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeClass(string size)
    {
        return size switch
        {
            "xs" => "text-xs",
            "sm" => "text-sm",
            "base" => "text-base",
            "lg" => "text-lg",
            "xl" => "text-xl",
            "2xl" => "text-2xl",
            "3xl" => "text-3xl",
            "4xl" => "text-4xl",
            _ when size.StartsWith("text-") => size,
            _ when size.Length > 0 => $"text-{size}",
            _ => string.Empty
        };
    }
}
