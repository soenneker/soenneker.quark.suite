using Soenneker.Quark.Enums;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance overflow builder with fluent API for chaining overflow rules.
/// </summary>
public sealed class OverflowBuilder : ICssBuilder
{
    private readonly List<OverflowRule> _rules = new(4);
    private string _axis = "";

    // ----- Class name constants -----
    private const string _classAuto = "overflow-auto";
    private const string _classHidden = "overflow-hidden";
    private const string _classVisible = "overflow-visible";
    private const string _classScroll = "overflow-scroll";
    private const string _classAutoX = "overflow-x-auto";
    private const string _classHiddenX = "overflow-x-hidden";
    private const string _classVisibleX = "overflow-x-visible";
    private const string _classScrollX = "overflow-x-scroll";
    private const string _classAutoY = "overflow-y-auto";
    private const string _classHiddenY = "overflow-y-hidden";
    private const string _classVisibleY = "overflow-y-visible";
    private const string _classScrollY = "overflow-y-scroll";

    // ----- CSS prefixes -----
    private const string _overflowPrefix = "overflow: ";
    private const string _overflowXPrefix = "overflow-x: ";
    private const string _overflowYPrefix = "overflow-y: ";

    internal OverflowBuilder(string overflow)
    {
        _rules.Add(new OverflowRule(overflow, null));
    }

    internal OverflowBuilder(List<OverflowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Creates a new OverflowBuilder with no initial value.
    /// </summary>
    public static OverflowBuilder Create()
    {
        return new OverflowBuilder(new List<OverflowRule>());
    }

    // ----- Fluent chaining (values & keywords) -----
    public OverflowBuilder Auto => Chain(OverflowKeyword.AutoValue);
    public OverflowBuilder Hidden => Chain(OverflowKeyword.HiddenValue);
    public OverflowBuilder Visible => Chain(OverflowKeyword.VisibleValue);
    public OverflowBuilder Scroll => Chain(OverflowKeyword.ScrollValue);

    public OverflowBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public OverflowBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public OverflowBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public OverflowBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public OverflowBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    // ----- Axis chaining -----
    public OverflowBuilder X => ChainAxis("-x");
    public OverflowBuilder Y => ChainAxis("-y");
    public OverflowBuilder All => ChainAxis("");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OverflowBuilder Chain(string overflow)
    {
        // Replace the last rule instead of adding a new one
        if (_rules.Count > 0)
        {
            _rules[_rules.Count - 1] = new OverflowRule(overflow, null);
        }
        else
        {
            _rules.Add(new OverflowRule(overflow, null));
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OverflowBuilder ChainAxis(string axis)
    {
        _axis = axis;
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

            var baseClass = GetOverflowClass(rule.Overflow);
            if (baseClass.Length == 0)
                continue;

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
            var value = _rules[i].Overflow;
            if (string.IsNullOrEmpty(value))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(GetOverflowPrefix());
            sb.Append(value);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetOverflowClass(string overflow)
    {
        var baseClass = overflow switch
        {
            OverflowKeyword.AutoValue => "overflow-auto",
            OverflowKeyword.HiddenValue => "overflow-hidden",
            OverflowKeyword.VisibleValue => "overflow-visible",
            OverflowKeyword.ScrollValue => "overflow-scroll",
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(baseClass) || string.IsNullOrEmpty(_axis))
            return baseClass;

        // Insert axis into class name: "overflow-auto" + "-x" = "overflow-x-auto"
        var dashIndex = baseClass.IndexOf('-');
        if (dashIndex > 0)
        {
            return baseClass.Insert(dashIndex, _axis);
        }

        return baseClass;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetOverflowPrefix()
    {
        return _axis switch
        {
            "-x" => _overflowXPrefix,
            "-y" => _overflowYPrefix,
            _ => _overflowPrefix
        };
    }

    /// <summary>Gets the string representation of the builder (same as ToClass).</summary>
    public override string ToString()
    {
        return ToClass();
    }
}
