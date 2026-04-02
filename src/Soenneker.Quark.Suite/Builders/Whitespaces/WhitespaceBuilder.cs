using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Tailwind whitespace utility builder.
/// </summary>
[TailwindPrefix("whitespace-", Responsive = true)]
public sealed class WhitespaceBuilder : ICssBuilder
{
    private readonly List<WhitespaceRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal WhitespaceBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new WhitespaceRule(value, breakpoint));
    }

    internal WhitespaceBuilder(List<WhitespaceRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the whitespace to normal.
    /// </summary>
    public WhitespaceBuilder Normal => Chain("normal");

    /// <summary>
    /// Sets the whitespace to nowrap.
    /// </summary>
    public WhitespaceBuilder Nowrap => Chain("nowrap");

    /// <summary>
    /// Sets the whitespace to pre.
    /// </summary>
    public WhitespaceBuilder Pre => Chain("pre");

    /// <summary>
    /// Sets the whitespace to pre-line.
    /// </summary>
    public WhitespaceBuilder PreLine => Chain("pre-line");

    /// <summary>
    /// Sets the whitespace to pre-wrap.
    /// </summary>
    public WhitespaceBuilder PreWrap => Chain("pre-wrap");

    /// <summary>
    /// Sets the whitespace to break-spaces.
    /// </summary>
    public WhitespaceBuilder BreakSpaces => Chain("break-spaces");

    public WhitespaceBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public WhitespaceBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public WhitespaceBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public WhitespaceBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public WhitespaceBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public WhitespaceBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WhitespaceBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new WhitespaceRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WhitespaceBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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

            sb.Append("white-space: ");
            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(string value)
    {
        return value switch
        {
            "normal" => "whitespace-normal",
            "nowrap" => "whitespace-nowrap",
            "pre" => "whitespace-pre",
            "pre-line" => "whitespace-pre-line",
            "pre-wrap" => "whitespace-pre-wrap",
            "break-spaces" => "whitespace-break-spaces",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyleValue(string value)
    {
        return value switch
        {
            "normal" => "normal",
            "nowrap" => "nowrap",
            "pre" => "pre",
            "pre-line" => "pre-line",
            "pre-wrap" => "pre-wrap",
            "break-spaces" => "break-spaces",
            _ => null
        };
    }
}
