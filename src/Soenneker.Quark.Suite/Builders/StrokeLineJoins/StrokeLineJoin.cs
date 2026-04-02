namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke line join. Tailwind: stroke-join-*.
/// </summary>
public static class StrokeLineJoin
{
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static StrokeLineJoinBuilder Auto => new("auto");
    /// <summary>
    /// Fluent step for `Round` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineJoinBuilder Round => new("round");
    /// <summary>
    /// Fluent step for `Bevel` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineJoinBuilder Bevel => new("bevel");
    /// <summary>
    /// Fluent step for `Miter` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static StrokeLineJoinBuilder Miter => new("miter");
}
