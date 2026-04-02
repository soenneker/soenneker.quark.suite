namespace Soenneker.Quark;

/// <summary>
/// Tailwind font family utility entry points.
/// </summary>
public static class FontFamily
{
    /// <summary>
    /// Fluent step for `Sans` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontFamilyBuilder Sans => new("sans");
    /// <summary>
    /// Fluent step for `Serif` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontFamilyBuilder Serif => new("serif");
    /// <summary>
    /// Fluent step for `Mono` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontFamilyBuilder Mono => new("mono");
}
