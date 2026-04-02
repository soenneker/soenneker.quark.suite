namespace Soenneker.Quark;

/// <summary>
/// Static utility for outline style. Tailwind: outline-none, outline, outline-dashed, outline-dotted, outline-double.
/// </summary>
public static class OutlineStyle
{
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static OutlineStyleBuilder None => new("none");
    /// <summary>
    /// Fluent step for `Solid` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OutlineStyleBuilder Solid => new("solid");
    /// <summary>
    /// Fluent step for `Dashed` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OutlineStyleBuilder Dashed => new("dashed");
    /// <summary>
    /// Fluent step for `Dotted` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OutlineStyleBuilder Dotted => new("dotted");
    /// <summary>
    /// Fluent step for `Double` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OutlineStyleBuilder Double => new("double");
}
