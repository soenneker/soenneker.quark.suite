namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll snap stop. Tailwind: snap-stop-normal, snap-stop-always.
/// </summary>
public static class ScrollSnapStop
{
    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapStopBuilder Normal => new("normal");
    /// <summary>
    /// Fluent step for `Always` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static ScrollSnapStopBuilder Always => new("always");
}
