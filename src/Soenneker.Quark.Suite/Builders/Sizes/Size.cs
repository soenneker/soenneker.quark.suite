namespace Soenneker.Quark;

/// <summary>
/// Size utility with fluent API.
/// Supports legacy semantic sizes and shadcn/Tailwind size-* tokens.
/// </summary>
public static class Size
{
    /// <summary>
    /// Default theme radius: `rounded` with no suffix — in Tailwind’s default config typically `0.25rem` (maps to shadcn `--radius` usage when you align tokens).
    /// </summary>
    public static SizeBuilder Default => new(SizeType.Default);
    /// <summary>
    /// Fluent step for `Extra Small` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder ExtraSmall => new(SizeType.ExtraSmall);
    /// <summary>
    /// Fluent step for `Small` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder Small => new(SizeType.Small);
    /// <summary>
    /// Fluent step for `Medium` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder Medium => new(SizeType.Medium);
    /// <summary>
    /// Fluent step for `Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder Large => new(SizeType.Large);
    /// <summary>
    /// Fluent step for `Extra Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder ExtraLarge => new(SizeType.ExtraLarge);
    /// <summary>
    /// Fluent step for `Extra Extra Large` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static SizeBuilder ExtraExtraLarge => new(SizeType.ExtraExtraLarge);

    // shadcn/Tailwind convenience (size-*)
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is0 => new("0");
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is1 => new("1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is2 => new("2");
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is3 => new("3");
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is4 => new("4");
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is5 => new("5");
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is6 => new("6");
    /// <summary>
    /// Spacing/sizing scale step `7` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 7` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is7 => new("7");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is8 => new("8");
    /// <summary>
    /// Spacing/sizing scale step `9` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 9` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is9 => new("9");
    /// <summary>
    /// Spacing/sizing scale step `10` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 10` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is10 => new("10");
    /// <summary>
    /// Spacing/sizing scale step `11` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 11` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is11 => new("11");
    /// <summary>
    /// Spacing/sizing scale step `12` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 12` for integer spacing utilities unless overridden).
    /// </summary>
    public static SizeBuilder Is12 => new("12");

    public static SizeBuilder From(SizeType sizeType) => new(sizeType);

    /// <summary>
    /// Create from an arbitrary Tailwind size token (e.g. "5", "full", "[18px]").
    /// </summary>
    public static SizeBuilder Token(string value) => new(value);
}
