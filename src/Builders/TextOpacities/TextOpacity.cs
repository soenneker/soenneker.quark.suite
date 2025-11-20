namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text opacity builders with predefined values.
/// </summary>
public static class TextOpacity
{
    /// <summary>
    /// Gets a text opacity builder with value 25 (25% opacity).
    /// </summary>
    public static TextOpacityBuilder V25 => new(25);
    /// <summary>
    /// Gets a text opacity builder with value 50 (50% opacity).
    /// </summary>
    public static TextOpacityBuilder V50 => new(50);
    /// <summary>
    /// Gets a text opacity builder with value 75 (75% opacity).
    /// </summary>
    public static TextOpacityBuilder V75 => new(75);
    /// <summary>
    /// Gets a text opacity builder with value 100 (fully opaque).
    /// </summary>
    public static TextOpacityBuilder V100 => new(100);
}






