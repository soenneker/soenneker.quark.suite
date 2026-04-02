namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap. Tailwind: snap-*.
/// </summary>
public static class ScrollSnap
{
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static ScrollSnapBuilder None => new("none");
    /// <summary>
    /// Fluent step for `X` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapBuilder X => new("x");
    /// <summary>
    /// Fluent step for `Y` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapBuilder Y => new("y");
    /// <summary>
    /// Fluent step for `Both` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapBuilder Both => new("both");
    /// <summary>
    /// Fluent step for `Mandatory` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapBuilder Mandatory => new("mandatory");
    /// <summary>
    /// Fluent step for `Proximity` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapBuilder Proximity => new("proximity");
}
