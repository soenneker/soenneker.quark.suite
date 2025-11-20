using Soenneker.Quark.Enums;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scale builder with fluent API for chaining scale rules.
/// </summary>
public sealed class ScaleBuilder : ICssBuilder
{
    private readonly List<ScaleRule> _rules = new(4);

    private const string _classScale0 = "scale-0";
    private const string _classScale1 = "scale-1";
    private const string _classScale2 = "scale-2";
    private const string _classScale3 = "scale-3";
    private const string _classScale4 = "scale-4";
    private const string _classScale5 = "scale-5";
    private const string _classScale6 = "scale-6";

    internal ScaleBuilder(ScaleType scale)
    {
        _rules.Add(new ScaleRule(scale));
    }

    internal ScaleBuilder(List<ScaleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the scale to 0.
    /// </summary>
    public ScaleBuilder Is0 => ChainWithScale(ScaleType.Is0);
    /// <summary>
    /// Sets the scale to 1.
    /// </summary>
    public ScaleBuilder Is1 => ChainWithScale(ScaleType.Is1);
    /// <summary>
    /// Sets the scale to 2.
    /// </summary>
    public ScaleBuilder Is2 => ChainWithScale(ScaleType.Is2);
    /// <summary>
    /// Sets the scale to 3.
    /// </summary>
    public ScaleBuilder Is3 => ChainWithScale(ScaleType.Is3);
    /// <summary>
    /// Sets the scale to 4.
    /// </summary>
    public ScaleBuilder Is4 => ChainWithScale(ScaleType.Is4);
    /// <summary>
    /// Sets the scale to 5.
    /// </summary>
    public ScaleBuilder Is5 => ChainWithScale(ScaleType.Is5);
    /// <summary>
    /// Sets the scale to 6.
    /// </summary>
    public ScaleBuilder Is6 => ChainWithScale(ScaleType.Is6);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScaleBuilder ChainWithScale(ScaleType scale)
    {
        _rules.Add(new ScaleRule(scale));
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
            var cls = GetScaleClass(rule.Scale);
            if (cls.Length == 0)
                continue;

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
            var scaleValue = GetScaleValue(rule.Scale);

            if (scaleValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("transform: scale(");
            sb.Append(scaleValue);
            sb.Append(")");
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetScaleClass(ScaleType scale)
    {
        return scale.Value switch
        {
            "0" => _classScale0,
            "1" => _classScale1,
            "2" => _classScale2,
            "3" => _classScale3,
            "4" => _classScale4,
            "5" => _classScale5,
            "6" => _classScale6,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetScaleValue(ScaleType scale)
    {
        return scale.Value switch
        {
            "0" => "0",
            "1" => "1",
            "2" => "2",
            "3" => "3",
            "4" => "4",
            "5" => "5",
            "6" => "6",
            _ => null
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}
