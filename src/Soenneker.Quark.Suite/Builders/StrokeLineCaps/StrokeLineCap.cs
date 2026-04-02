namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke line cap. Tailwind: stroke-cap-*.
/// </summary>
public static class StrokeLineCap
{
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static StrokeLineCapBuilder Auto => new("auto");
    /// <summary>
    /// Fluent step for `Round` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineCapBuilder Round => new("round");
    /// <summary>
    /// Fluent step for `Square` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineCapBuilder Square => new("square");
    /// <summary>
    /// Fluent step for `Butt` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineCapBuilder Butt => new("butt");
}
