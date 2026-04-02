namespace Soenneker.Quark;

/// <summary>
/// Grid row utility with fluent responsive chaining.
/// </summary>
public static class Rows
{
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is1 => new("grid-rows", "1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is2 => new("grid-rows", "2");
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is3 => new("grid-rows", "3");
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is4 => new("grid-rows", "4");
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is5 => new("grid-rows", "5");
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public static RowBuilder Is6 => new("grid-rows", "6");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static RowBuilder None => new("grid-rows", "none");
    /// <summary>
    /// Fluent step for `Subgrid` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static RowBuilder Subgrid => new("grid-rows", "subgrid");

    /// <summary>
    /// Create from a numeric grid row count.
    /// </summary>
    public static RowBuilder Count(int value) => new("grid-rows", value.ToString());
}
