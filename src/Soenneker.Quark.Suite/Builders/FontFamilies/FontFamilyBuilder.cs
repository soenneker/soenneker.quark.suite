using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Tailwind font family utility builder.
/// </summary>
[TailwindPrefix("font-", Responsive = true)]
public sealed class FontFamilyBuilder : ICssBuilder
{
    private readonly List<FontFamilyRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal FontFamilyBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FontFamilyRule(value, breakpoint));
    }

    internal FontFamilyBuilder(List<FontFamilyRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the font family to sans.
    /// </summary>
    public FontFamilyBuilder Sans => Chain("sans");

    /// <summary>
    /// Sets the font family to serif.
    /// </summary>
    public FontFamilyBuilder Serif => Chain("serif");

    /// <summary>
    /// Sets the font family to mono.
    /// </summary>
    public FontFamilyBuilder Mono => Chain("mono");

    public FontFamilyBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public FontFamilyBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public FontFamilyBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public FontFamilyBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public FontFamilyBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public FontFamilyBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontFamilyBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new FontFamilyRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontFamilyBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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

            sb.Append("font-family: ");
            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(string value)
    {
        return value switch
        {
            "sans" => "font-sans",
            "serif" => "font-serif",
            "mono" => "font-mono",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyleValue(string value)
    {
        return value switch
        {
            "sans" => "ui-sans-serif, system-ui, sans-serif",
            "serif" => "ui-serif, Georgia, serif",
            "mono" => "ui-monospace, SFMono-Regular, monospace",
            _ => null
        };
    }
}
