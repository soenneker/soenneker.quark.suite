using Soenneker.Quark.Enums;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap text utilities.
/// </summary>
public sealed class TextStyleBuilder : ICssBuilder
{
    private readonly List<TextStyleRule> _rules = new(4);

    internal TextStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextStyleRule(value, breakpoint));
    }

    internal TextStyleBuilder(List<TextStyleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public bool IsEmpty => _rules.Count == 0;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    // Wrapping
    public TextStyleBuilder Wrap => ChainValue("wrap");
    public TextStyleBuilder Nowrap => ChainValue("nowrap");
    public TextStyleBuilder Truncate => ChainValue("truncate");

    // Transformation
    public TextStyleBuilder Lowercase => ChainValue("lowercase");
    public TextStyleBuilder Uppercase => ChainValue("uppercase");
    public TextStyleBuilder Capitalize => ChainValue("capitalize");

    // Styling
    public TextStyleBuilder Reset => ChainValue("reset");
    public TextStyleBuilder Muted => ChainValue("muted");
    public TextStyleBuilder Emphasis => ChainValue("emphasis");

    // Breakpoints
    public TextStyleBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextStyleBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextStyleBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextStyleBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextStyleBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder ChainValue(string value)
    {
        var newRules = new List<TextStyleRule>(_rules) { new(value, null) };
        return new TextStyleBuilder(newRules);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder ChainBp(BreakpointType bp)
    {
        var newRules = new List<TextStyleRule>(_rules);
        if (newRules.Count == 0)
        {
            newRules.Add(new TextStyleRule("inherit", bp));
            return new TextStyleBuilder(newRules);
        }

        var lastIdx = newRules.Count - 1;
        var last = newRules[lastIdx];
        newRules[lastIdx] = new TextStyleRule(last.Value, bp);
        return new TextStyleBuilder(newRules);
    }

    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new Utils.PooledStringBuilders.PooledStringBuilder();
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

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextStyleRule styleRule)
    {
        return styleRule.Value switch
        {
            "wrap" => "text-wrap",
            "nowrap" => "text-nowrap",
            "truncate" => "text-truncate",
            "lowercase" => "text-lowercase",
            "uppercase" => "text-uppercase",
            "capitalize" => "text-capitalize",
            "reset" => "text-reset",
            "muted" => "text-muted",
            "emphasis" => "text-emphasis",
            _ => string.Empty
        };
    }


    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}