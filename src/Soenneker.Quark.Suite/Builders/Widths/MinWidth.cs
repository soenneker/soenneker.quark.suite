namespace Soenneker.Quark;

/// <summary>
/// Min-width utility with fluent API. Tailwind-first (min-w-*).
/// </summary>
public static class MinWidth
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static MinWidthBuilder Is0 => new("0");
    /// <summary>
    /// Fluent step for `Is Px` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MinWidthBuilder IsPx => new("px");
    /// <summary>
    /// Fluent step for `Is Full` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MinWidthBuilder IsFull => new("full");
    /// <summary>
    /// Fluent step for `Is Min` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MinWidthBuilder IsMin => new("min");
    /// <summary>
    /// Fluent step for `Is Max` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MinWidthBuilder IsMax => new("max");
    /// <summary>
    /// Fluent step for `Is Fit` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MinWidthBuilder IsFit => new("fit");

    /// <summary>
    /// Create from an arbitrary Tailwind min-width token (e.g. "24", "[8rem]", "[12rem]").
    /// </summary>
    public static MinWidthBuilder Token(string token) => new(token);
}
