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

    /// <summary>
    /// Default theme radius: `rounded` with no suffix — in Tailwind’s default config typically `0.25rem` (maps to shadcn `--radius` usage when you align tokens).
    /// </summary>
    public SizeBuilder Default => ChainWithValue(SizeType.Default.Value);
    /// <summary>
    /// Fluent step for `Extra Small` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder ExtraSmall => ChainWithValue(SizeType.ExtraSmall.Value);
    /// <summary>
    /// Fluent step for `Small` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder Small => ChainWithValue(SizeType.Small.Value);
    /// <summary>
    /// Fluent step for `Medium` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder Medium => ChainWithValue(SizeType.Medium.Value);
    /// <summary>
    /// Fluent step for `Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder Large => ChainWithValue(SizeType.Large.Value);
    /// <summary>
    /// Fluent step for `Extra Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder ExtraLarge => ChainWithValue(SizeType.ExtraLarge.Value);
    /// <summary>
    /// Fluent step for `Extra Extra Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public SizeBuilder ExtraExtraLarge => ChainWithValue(SizeType.ExtraExtraLarge.Value);

    // shadcn/Tailwind size-* helpers
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is0 => ChainWithValue("0");
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is1 => ChainWithValue("1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is2 => ChainWithValue("2");
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is3 => ChainWithValue("3");
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is4 => ChainWithValue("4");
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is5 => ChainWithValue("5");
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is6 => ChainWithValue("6");
    /// <summary>
    /// Spacing/sizing scale step `7` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 7` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is7 => ChainWithValue("7");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is8 => ChainWithValue("8");
    /// <summary>
    /// Spacing/sizing scale step `9` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 9` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is9 => ChainWithValue("9");
    /// <summary>
    /// Spacing/sizing scale step `10` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 10` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is10 => ChainWithValue("10");
    /// <summary>
    /// Spacing/sizing scale step `11` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 11` for integer spacing utilities unless overridden).
    /// </summary>
    public SizeBuilder Is11 => ChainWithValue("11");
    /// <summary>
    /// Spacing/sizing scale step `12` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 12` for integer spacing utilities unless overridden).
    /// </summary>
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
