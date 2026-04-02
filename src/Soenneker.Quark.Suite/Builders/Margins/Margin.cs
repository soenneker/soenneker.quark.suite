namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned margin utility.
/// </summary>
public static class Margin
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is0 => new(ScaleType.Is0Value);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is1 => new(ScaleType.Is1Value);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is2 => new(ScaleType.Is2Value);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is3 => new(ScaleType.Is3Value);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is4 => new(ScaleType.Is4Value);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is5 => new(ScaleType.Is5Value);
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public static MarginBuilder Is8 => new("8");
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static MarginBuilder Auto => new("auto");

    /// <summary>
    /// Creates a margin utility from an arbitrary Tailwind spacing token.
    /// </summary>
    public static MarginBuilder Token(string value) => new(value);
}
