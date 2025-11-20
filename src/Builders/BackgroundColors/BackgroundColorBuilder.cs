using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance background color builder.
/// Produces Bootstrap utility classes when possible, otherwise falls back to inline style.
/// </summary>
public sealed class BackgroundColorBuilder : ICssBuilder
{
    private readonly List<BackgroundColorRule> _rules = new(4);

    internal BackgroundColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundColorRule(value, breakpoint));
    }

    internal BackgroundColorBuilder(List<BackgroundColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BackgroundColorBuilder Primary => ChainValue("primary");
    public BackgroundColorBuilder Secondary => ChainValue("secondary");
    public BackgroundColorBuilder Success => ChainValue("success");
    public BackgroundColorBuilder Danger => ChainValue("danger");
    public BackgroundColorBuilder Warning => ChainValue("warning");
    public BackgroundColorBuilder Info => ChainValue("info");
    public BackgroundColorBuilder Light => ChainValue("light");
    public BackgroundColorBuilder Dark => ChainValue("dark");
    public BackgroundColorBuilder Link => ChainValue("link");
    public BackgroundColorBuilder Muted => ChainValue("muted");

    public BackgroundColorBuilder White => ChainValue("white");

    public BackgroundColorBuilder Black => ChainValue("black");

    public BackgroundColorBuilder Transparent => ChainValue("transparent");

    public BackgroundColorBuilder Inherit => ChainValue(GlobalKeyword.InheritValue);
    public BackgroundColorBuilder Initial => ChainValue(GlobalKeyword.InitialValue);
    public BackgroundColorBuilder Revert => ChainValue(GlobalKeyword.RevertValue);
    public BackgroundColorBuilder RevertLayer => ChainValue(GlobalKeyword.RevertLayerValue);
    public BackgroundColorBuilder Unset => ChainValue(GlobalKeyword.UnsetValue);

    public BackgroundColorBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public BackgroundColorBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public BackgroundColorBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public BackgroundColorBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public BackgroundColorBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public BackgroundColorBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainValue(string value)
    {
        _rules.Add(new BackgroundColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BackgroundColorRule("inherit", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BackgroundColorRule(last.Value, bp);
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
            var cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

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
            var rule = _rules[i];
            var css = GetStyle(rule);
            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the string representation of the background color builder.
    /// Chooses between class and style based on whether any rules generate CSS classes.
    /// </summary>
    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        // First try to generate classes
        var classResult = ToClass();
        if (classResult.HasContent())
            return classResult;

        // Fall back to styles if no classes were generated
        return ToStyle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(BackgroundColorRule rule)
    {
        return rule.Value switch
        {
            "primary" or "secondary" or "success" or "danger" or "warning" or "info" or "light" or "dark" or "link" or "muted" or "white"
                or "black" or "transparent" => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(BackgroundColorRule rule)
    {
        return rule.Value switch
        {
            "primary" or "secondary" or "success" or "danger" or "warning" or "info" or "light" or "dark" or "link" or "muted" or "white" or "black" or "transparent" => null,
            _ => rule.Value
        };
    }

}
