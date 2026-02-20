using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified width builder with fluent API for chaining width rules.
/// </summary>
public sealed class WidthBuilder : ICssBuilder
{
    private readonly List<WidthRule> _rules = new(4);

    private const string _classW25 = "w-25";
    private const string _classW50 = "w-50";
    private const string _classW75 = "w-75";
    private const string _classW100 = "w-100";
    private const string _classWAuto = "w-auto";
    private const string _widthPrefix = "width: ";

    internal WidthBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new WidthRule(size, breakpoint));
    }

    internal WidthBuilder(List<WidthRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the width to 25%.
	/// </summary>
    public WidthBuilder Is25 => ChainWithSize("25");
	/// <summary>
	/// Sets the width to 50%.
	/// </summary>
    public WidthBuilder Is50 => ChainWithSize("50");
	/// <summary>
	/// Sets the width to 75%.
	/// </summary>
    public WidthBuilder Is75 => ChainWithSize("75");
	/// <summary>
	/// Sets the width to 100%.
	/// </summary>
    public WidthBuilder Is100 => ChainWithSize("100");
	/// <summary>
	/// Sets the width to auto.
	/// </summary>
    public WidthBuilder Auto => ChainWithSize("auto");

	/// <summary>
	/// Applies the width on phone breakpoint.
	/// </summary>
    public WidthBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the width on tablet breakpoint.
	/// </summary>
    public WidthBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the width on laptop breakpoint.
	/// </summary>
    public WidthBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the width on desktop breakpoint.
	/// </summary>
    public WidthBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the width on widescreen breakpoint.
	/// </summary>
    public WidthBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
	/// <summary>
	/// Applies the width on ultrawide breakpoint.
	/// </summary>
    public WidthBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder ChainWithSize(string size)
    {
        _rules.Add(new WidthRule(size, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WidthBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new WidthRule("100", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new WidthRule(last.Size, breakpoint);
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
            var cls = GetWidthClass(rule.Size);
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

	/// <summary>
	/// Gets the CSS style string for the current configuration.
	/// </summary>
	/// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = GetWidthValue(rule.Size);
            if (val is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_widthPrefix);
            sb.Append(val);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetWidthClass(string size)
    {
        return size switch
        {
            "25" => _classW25,
            "50" => _classW50,
            "75" => _classW75,
            "100" => _classW100,
            "auto" => _classWAuto,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetWidthValue(string size)
    {
        return size switch
        {
            "25" => "25%",
            "50" => "50%",
            "75" => "75%",
            "100" => "100%",
            "auto" => "auto",
            _ => size
        };
    }


}

