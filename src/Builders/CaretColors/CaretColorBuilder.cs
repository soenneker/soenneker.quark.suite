using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Caret color builder for text inputs. Tailwind: caret-primary, caret-transparent, caret-*.
/// </summary>
public sealed class CaretColorBuilder : ICssBuilder
{
    private readonly List<CaretColorRule> _rules = new(4);

    internal CaretColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new CaretColorRule(value, breakpoint));
    }

    internal CaretColorBuilder(List<CaretColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public CaretColorBuilder Primary => Chain("primary");
    public CaretColorBuilder Transparent => Chain("transparent");
    public CaretColorBuilder Current => Chain("current");
    public CaretColorBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public CaretColorBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public CaretColorBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public CaretColorBuilder OnSm => ChainBp(BreakpointType.Sm);
    public CaretColorBuilder OnMd => ChainBp(BreakpointType.Md);
    public CaretColorBuilder OnLg => ChainBp(BreakpointType.Lg);
    public CaretColorBuilder OnXl => ChainBp(BreakpointType.Xl);
    public CaretColorBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CaretColorBuilder Chain(string value)
    {
        _rules.Add(new CaretColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CaretColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new CaretColorRule("primary", bp));
        else _rules[^1] = new CaretColorRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = GetClass(rule.Value);
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
        }
        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var styleVal = rule.Value switch
            {
                "primary" => "var(--primary)",
                "transparent" => "transparent",
                "current" => "currentColor",
                "inherit" => "inherit",
                "initial" => "initial",
                "unset" => "unset",
                _ => rule.Value.StartsWith("--") || rule.Value.StartsWith("#") || rule.Value.StartsWith("rgb") ? rule.Value : null
            };
            if (styleVal is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("caret-color: ");
            sb.Append(styleVal);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v) => v switch
    {
        "primary" => "caret-primary",
        "transparent" => "caret-transparent",
        "current" => "caret-current",
        "inherit" => "caret-inherit",
        "initial" => "caret-initial",
        "unset" => "caret-unset",
        _ => v.StartsWith("caret-") ? v : string.Empty
    };
}
