namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned border width utility.
/// </summary>
public static class Border
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is0 => new(ScaleType.Is0.Value);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is1 => new(ScaleType.Is1.Value);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is2 => new(ScaleType.Is2.Value);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is3 => new(ScaleType.Is3.Value);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is4 => new(ScaleType.Is4.Value);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static BorderBuilder Is5 => new(ScaleType.Is5.Value);

    /// <summary>
    /// Creates a border width utility from an arbitrary Tailwind token.
    /// </summary>
    public static BorderBuilder Token(string value) => new(value);
}
