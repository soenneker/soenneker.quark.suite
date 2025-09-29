using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;
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

    public ScaleBuilder S0 => ChainWithScale(ScaleType.S0);
    public ScaleBuilder S1 => ChainWithScale(ScaleType.S1);
    public ScaleBuilder S2 => ChainWithScale(ScaleType.S2);
    public ScaleBuilder S3 => ChainWithScale(ScaleType.S3);
    public ScaleBuilder S4 => ChainWithScale(ScaleType.S4);
    public ScaleBuilder S5 => ChainWithScale(ScaleType.S5);
    public ScaleBuilder S6 => ChainWithScale(ScaleType.S6);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScaleBuilder ChainWithScale(ScaleType scale)
    {
        _rules.Add(new ScaleRule(scale));
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
            ScaleRule rule = _rules[i];
            string cls = GetScaleClass(rule.Scale);
            if (cls.Length == 0)
                continue;

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
            ScaleRule rule = _rules[i];
            string? scaleValue = GetScaleValue(rule.Scale);

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
