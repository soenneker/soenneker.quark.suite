namespace Soenneker.Quark;

/// <summary>
/// Tailwind ring utility.
/// </summary>
public static class Ring
{
    /// <summary>
    /// Default theme radius: `rounded` with no suffix — in Tailwind’s default config typically `0.25rem` (maps to shadcn `--radius` usage when you align tokens).
    /// </summary>
    public static RingBuilder Default => new("ring");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static RingBuilder None => new("0");
    /// <summary>
    /// Fluent step for `One` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RingBuilder One => new("1");
    /// <summary>
    /// Fluent step for `Two` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RingBuilder Two => new("2");
    /// <summary>
    /// Fluent step for `Four` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RingBuilder Four => new("4");
    /// <summary>
    /// Fluent step for `Eight` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RingBuilder Eight => new("8");
    /// <summary>
    /// Fluent step for `Inset` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RingBuilder Inset => new("inset");
}
