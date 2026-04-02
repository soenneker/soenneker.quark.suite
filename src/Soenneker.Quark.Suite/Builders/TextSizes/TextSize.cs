namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned text size utility.
/// </summary>
public static class TextSize
{
    /// <summary>
    /// Fluent step for `Xs` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static TextSizeBuilder Xs => new("xs");
    /// <summary>
    /// `rounded-sm` — small radius (default theme `0.125rem`).
    /// </summary>
    public static TextSizeBuilder Sm => new("sm");
    /// <summary>
    /// Fluent step for `Base` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static TextSizeBuilder Base => new("base");
    /// <summary>
    /// `rounded-lg` — large radius (default theme `0.5rem`).
    /// </summary>
    public static TextSizeBuilder Lg => new("lg");
    /// <summary>
    /// `rounded-xl` — extra-large radius (default theme `0.75rem`).
    /// </summary>
    public static TextSizeBuilder Xl => new("xl");
    /// <summary>
    /// `rounded-2xl` — 2× XL radius (default theme `1rem`).
    /// </summary>
    public static TextSizeBuilder TwoXl => new("2xl");
    /// <summary>
    /// `rounded-3xl` — very large radius (default theme `1.5rem`).
    /// </summary>
    public static TextSizeBuilder ThreeXl => new("3xl");
    /// <summary>
    /// Fluent step for `Four Xl` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static TextSizeBuilder FourXl => new("4xl");

    /// <summary>
    /// Creates an arbitrary Tailwind text size token (e.g. "5xl", "[18px]").
    /// </summary>
    public static TextSizeBuilder Token(string value) => new(value);
}
