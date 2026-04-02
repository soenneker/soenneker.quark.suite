namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll margin. Tailwind: scroll-m-*.
/// </summary>
public static class ScrollMargin
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is0 => new(ScaleType.Is0Value);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is1 => new(ScaleType.Is1Value);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is2 => new(ScaleType.Is2Value);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is3 => new(ScaleType.Is3Value);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is4 => new(ScaleType.Is4Value);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is5 => new(ScaleType.Is5Value);
    /// <summary>
    /// Spacing/sizing scale step `24` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 24` for integer spacing utilities unless overridden).
    /// </summary>
    public static ScrollMarginBuilder Is24 => new("24");
    /// <summary>
    /// One pixel (`px` unit) — hairline borders, fixed 1px tracks, etc.
    /// </summary>
    public static ScrollMarginBuilder Px => new("px");
}
