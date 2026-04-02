namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap align. Tailwind: snap-start, snap-center, snap-end, snap-align-none.
/// </summary>
public static class ScrollSnapAlign
{
    /// <summary>
    /// Fluent step for `Start` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapAlignBuilder Start => new("start");
    /// <summary>
    /// Fluent step for `Center` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapAlignBuilder Center => new("center");
    /// <summary>
    /// Fluent step for `End` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapAlignBuilder End => new("end");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static ScrollSnapAlignBuilder None => new("none");
}
