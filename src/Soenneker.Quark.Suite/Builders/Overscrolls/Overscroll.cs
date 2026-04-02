
namespace Soenneker.Quark;

/// <summary>
/// Tailwind overscroll-behavior utility entry points.
/// </summary>
public static class Overscroll
{
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static OverscrollBuilder Auto => new("auto");
    /// <summary>
    /// Fluent step for `Contain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder Contain => new("contain");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static OverscrollBuilder None => new("none");

    /// <summary>
    /// Fluent step for `XAuto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder XAuto => new("x-auto");
    /// <summary>
    /// Fluent step for `XContain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder XContain => new("x-contain");
    /// <summary>
    /// Fluent step for `XNone` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder XNone => new("x-none");

    /// <summary>
    /// Fluent step for `YAuto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder YAuto => new("y-auto");
    /// <summary>
    /// Fluent step for `YContain` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder YContain => new("y-contain");
    /// <summary>
    /// Fluent step for `YNone` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static OverscrollBuilder YNone => new("y-none");
}
