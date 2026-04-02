using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Size builder with fluent API for chaining size rules.
/// Supports both legacy semantic tokens (sm/lg) and shadcn/Tailwind size utilities (e.g. size-5).
/// </summary>
[TailwindPrefix("size-", Responsive = true)]
public sealed class SizeBuilder : ICssBuilder
{
    private readonly List<SizeRule> _rules = new(4);

    internal SizeBuilder(SizeType size)
    {
        _rules.Add(new SizeRule(size.Value));
    }

    internal SizeBuilder(string value)
    {
        _rules.Add(new SizeRule(value));
    }

    internal SizeBuilder(List<SizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public SizeBuilder Default => ChainWithValue(SizeType.Default.Value);
    public SizeBuilder ExtraSmall => ChainWithValue(SizeType.ExtraSmall.Value);
    public SizeBuilder Small => ChainWithValue(SizeType.Small.Value);
    public SizeBuilder Medium => ChainWithValue(SizeType.Medium.Value);
    public SizeBuilder Large => ChainWithValue(SizeType.Large.Value);
    public SizeBuilder ExtraLarge => ChainWithValue(SizeType.ExtraLarge.Value);
    public SizeBuilder ExtraExtraLarge => ChainWithValue(SizeType.ExtraExtraLarge.Value);

    // shadcn/Tailwind size-* helpers
    public SizeBuilder Is0 => ChainWithValue("0");
    public SizeBuilder Is1 => ChainWithValue("1");
    public SizeBuilder Is2 => ChainWithValue("2");
    public SizeBuilder Is3 => ChainWithValue("3");
    public SizeBuilder Is4 => ChainWithValue("4");
    public SizeBuilder Is5 => ChainWithValue("5");
    public SizeBuilder Is6 => ChainWithValue("6");
    public SizeBuilder Is7 => ChainWithValue("7");
    public SizeBuilder Is8 => ChainWithValue("8");
    public SizeBuilder Is9 => ChainWithValue("9");
    public SizeBuilder Is10 => ChainWithValue("10");
    public SizeBuilder Is11 => ChainWithValue("11");
    public SizeBuilder Is12 => ChainWithValue("12");

    /// <summary>
    /// Applies an arbitrary Tailwind size token (e.g. "4", "5", "[18px]", "full").
    /// </summary>
    public SizeBuilder Token(string value) => ChainWithValue(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SizeBuilder ChainWithValue(string value)
    {
        _rules.Add(new SizeRule(value));
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
            var cls = GetSizeClass(_rules[i].Value);
            if (cls.Length == 0)
                continue;

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeClass(string value)
    {
        return value switch
        {
            "" => string.Empty,
            // Legacy semantic tokens used by existing component size mapping logic
            "xs" => "xs",
            "sm" => "sm",
            "md" => "md",
            "lg" => "lg",
            "xl" => "xl",
            "2xl" => "2xl",

            // Common Tailwind size-* utilities (explicit literals for Tailwind CLI discovery)
            "0" => "size-0",
            "1" => "size-1",
            "2" => "size-2",
            "3" => "size-3",
            "4" => "size-4",
            "5" => "size-5",
            "6" => "size-6",
            "7" => "size-7",
            "8" => "size-8",
            "9" => "size-9",
            "10" => "size-10",
            "11" => "size-11",
            "12" => "size-12",
            "14" => "size-14",
            "16" => "size-16",
            "20" => "size-20",
            "24" => "size-24",
            "28" => "size-28",
            "32" => "size-32",
            "36" => "size-36",
            "40" => "size-40",
            "44" => "size-44",
            "48" => "size-48",
            "52" => "size-52",
            "56" => "size-56",
            "60" => "size-60",
            "64" => "size-64",
            "72" => "size-72",
            "80" => "size-80",
            "96" => "size-96",
            "px" => "size-px",
            "full" => "size-full",
            "auto" => "size-auto",

            _ when value.StartsWith("size-") => value,
            _ => string.Empty
        };
    }

    public override string ToString() => ToClass();
}
