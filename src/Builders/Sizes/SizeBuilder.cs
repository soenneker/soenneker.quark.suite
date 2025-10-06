using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Size builder with fluent API for chaining size rules.
/// </summary>
public sealed class SizeBuilder : ICssBuilder
{
    private readonly List<SizeRule> _rules = new(4);

    private const string _classSizeDefault = "";
    private const string _classSizeXs = "xs";
    private const string _classSizeSm = "sm";
    private const string _classSizeMd = "md";
    private const string _classSizeLg = "lg";
    private const string _classSizeXl = "xl";
    private const string _classSizeXxl = "xxl";

    internal SizeBuilder(SizeType size)
    {
        _rules.Add(new SizeRule(size));
    }

    internal SizeBuilder(List<SizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public SizeBuilder Default => ChainWithSize(SizeType.Default);
    public SizeBuilder ExtraSmall => ChainWithSize(SizeType.ExtraSmall);
    public SizeBuilder Small => ChainWithSize(SizeType.Small);
    public SizeBuilder Medium => ChainWithSize(SizeType.Medium);
    public SizeBuilder Large => ChainWithSize(SizeType.Large);
    public SizeBuilder ExtraLarge => ChainWithSize(SizeType.ExtraLarge);
    public SizeBuilder ExtraExtraLarge => ChainWithSize(SizeType.ExtraExtraLarge);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SizeBuilder ChainWithSize(SizeType size)
    {
        _rules.Add(new SizeRule(size));
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
            var cls = GetSizeClass(rule.Size);
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
            var rule = _rules[i];
            var sizeValue = GetSizeValue(rule.Size);

            if (sizeValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("size: ");
            sb.Append(sizeValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeClass(SizeType size)
    {
        return size.Value switch
        {
            "" => _classSizeDefault,
            "xs" => _classSizeXs,
            "sm" => _classSizeSm,
            "md" => _classSizeMd,
            "lg" => _classSizeLg,
            "xl" => _classSizeXl,
            "xxl" => _classSizeXxl,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(SizeType size)
    {
        return size.Value switch
        {
            "" => null, // Default size doesn't need CSS
            "xs" => "xs",
            "sm" => "sm",
            "md" => "md",
            "lg" => "lg",
            "xl" => "xl",
            "xxl" => "xxl",
            _ => null
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}
