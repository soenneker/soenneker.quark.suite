namespace Soenneker.Quark;

/// <summary>
/// Height utility with fluent API and Tailwind-first mappings.
/// </summary>
public static class Height
{
    /// <summary>
    /// Spacing/sizing scale step `25` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 25` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is25 => new("25");
    /// <summary>
    /// Spacing/sizing scale step `50` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 50` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is50 => new("50");
    /// <summary>
    /// Spacing/sizing scale step `75` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 75` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is75 => new("75");
    /// <summary>
    /// Spacing/sizing scale step `100` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 100` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is100 => new("100");

    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is0 => new("0");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is8 => new("8");
    /// <summary>
    /// Spacing/sizing scale step `16` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 16` for integer spacing utilities unless overridden).
    /// </summary>
    public static HeightBuilder Is16 => new("16");
    /// <summary>
    /// Fluent step for `Is Px` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsPx => new("px");
    /// <summary>
    /// Fluent step for `Is Full` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsFull => new("full");
    /// <summary>
    /// Fluent step for `Is Screen` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsScreen => new("screen");
    /// <summary>
    /// Fluent step for `Is Fit` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsFit => new("fit");
    /// <summary>
    /// Fluent step for `Is Min` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsMin => new("min");
    /// <summary>
    /// Fluent step for `Is Max` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder IsMax => new("max");

    /// <summary>
    /// Fluent step for `Is1of2` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder Is1of2 => new("1/2");
    /// <summary>
    /// Fluent step for `Is1of3` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder Is1of3 => new("1/3");
    /// <summary>
    /// Fluent step for `Is2of3` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder Is2of3 => new("2/3");
    /// <summary>
    /// Fluent step for `Is1of4` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder Is1of4 => new("1/4");
    /// <summary>
    /// Fluent step for `Is3of4` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static HeightBuilder Is3of4 => new("3/4");

    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static HeightBuilder Auto => new("auto");

    /// <summary>
    /// Create from an arbitrary Tailwind height token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public static HeightBuilder Token(string token) => new(token);
}
