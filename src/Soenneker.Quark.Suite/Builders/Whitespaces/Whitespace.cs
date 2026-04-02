namespace Soenneker.Quark;

/// <summary>
/// Tailwind whitespace utility entry points.
/// </summary>
public static class Whitespace
{
    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder Normal => new("normal");
    /// <summary>
    /// Fluent step for `Nowrap` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder Nowrap => new("nowrap");
    /// <summary>
    /// Fluent step for `Pre` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder Pre => new("pre");
    /// <summary>
    /// Fluent step for `Pre Line` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder PreLine => new("pre-line");
    /// <summary>
    /// Fluent step for `Pre Wrap` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder PreWrap => new("pre-wrap");
    /// <summary>
    /// Fluent step for `Break Spaces` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static WhitespaceBuilder BreakSpaces => new("break-spaces");
}
