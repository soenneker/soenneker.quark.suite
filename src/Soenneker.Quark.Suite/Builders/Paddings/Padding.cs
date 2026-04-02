namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned padding utility.
/// </summary>
public static class Padding
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is0 => new(ScaleType.Is0Value);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is1 => new(ScaleType.Is1Value);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is2 => new(ScaleType.Is2Value);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is3 => new(ScaleType.Is3Value);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is4 => new(ScaleType.Is4Value);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is5 => new(ScaleType.Is5Value);
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is6 => new("6");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is8 => new("8");
    /// <summary>
    /// Spacing/sizing scale step `16` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 16` for integer spacing utilities unless overridden).
    /// </summary>
    public static PaddingBuilder Is16 => new("16");

    /// <summary>
    /// Creates a padding utility from an arbitrary Tailwind spacing token.
    /// </summary>
    public static PaddingBuilder Token(string value) => new(value);
}
