namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating background opacity builders with predefined values.
/// </summary>
public static class BackgroundOpacity
{
    /// <summary>
    /// Gets a background opacity builder with value 10 (10% opacity).
    /// </summary>
    public static BackgroundOpacityBuilder Is10 => new(10);
    /// <summary>
    /// Gets a background opacity builder with value 25 (25% opacity).
    /// </summary>
    public static BackgroundOpacityBuilder Is25 => new(25);
    /// <summary>
    /// Gets a background opacity builder with value 50 (50% opacity).
    /// </summary>
    public static BackgroundOpacityBuilder Is50 => new(50);
    /// <summary>
    /// Gets a background opacity builder with value 75 (75% opacity).
    /// </summary>
    public static BackgroundOpacityBuilder Is75 => new(75);
    /// <summary>
    /// Gets a background opacity builder with value 100 (fully opaque).
    /// </summary>
    public static BackgroundOpacityBuilder Is100 => new(100);
}
