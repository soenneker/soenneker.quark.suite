namespace Soenneker.Quark;

public static class LetterSpacing
{
    /// <summary>
    /// Fluent step for `Tighter` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Tighter => new("tracking-tighter", "");
    /// <summary>
    /// Fluent step for `Tight` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Tight => new("tracking-tight", "");
    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Normal => new("tracking-normal", "");
    /// <summary>
    /// Fluent step for `Wide` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Wide => new("tracking-wide", "");
    /// <summary>
    /// Fluent step for `Wider` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Wider => new("tracking-wider", "");
    /// <summary>
    /// Fluent step for `Widest` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static LetterSpacingBuilder Widest => new("tracking-widest", "");
    /// <summary>
    /// Tailwind token segment (spacing scale step, arbitrary value like `[17rem]`, or theme key). Builds the matching utility class for this builder.
    /// </summary>
    /// <param name="value">Suffix/token after the utility prefix (see Tailwind docs for this family).</param>
    public static LetterSpacingBuilder Token(string value) => new("tracking", value);
}
