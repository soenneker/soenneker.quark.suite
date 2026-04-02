namespace Soenneker.Quark;

/// <summary>
/// Static utility for contain. Tailwind: contain-*.
/// </summary>
public static class Contain
{
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static ContainBuilder None => new("none");
    /// <summary>
    /// Fluent step for `Size` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Size => new("size");
    /// <summary>
    /// Fluent step for `Layout` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Layout => new("layout");
    /// <summary>
    /// Fluent step for `Style` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Style => new("style");
    /// <summary>
    /// Fluent step for `Paint` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Paint => new("paint");
    /// <summary>
    /// Fluent step for `Strict` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Strict => new("strict");
    /// <summary>
    /// Fluent step for `Content` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ContainBuilder Content => new("content");
}
