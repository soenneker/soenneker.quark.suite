namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating link opacity builders with predefined values.
/// </summary>
public static class LinkOpacity
{
    /// <summary>
    /// Gets a link opacity builder with value 10 (10% opacity).
    /// </summary>
    public static LinkOpacityBuilder V10 => new(10);
    /// <summary>
    /// Gets a link opacity builder with value 25 (25% opacity).
    /// </summary>
    public static LinkOpacityBuilder V25 => new(25);
    /// <summary>
    /// Gets a link opacity builder with value 50 (50% opacity).
    /// </summary>
    public static LinkOpacityBuilder V50 => new(50);
    /// <summary>
    /// Gets a link opacity builder with value 75 (75% opacity).
    /// </summary>
    public static LinkOpacityBuilder V75 => new(75);
    /// <summary>
    /// Gets a link opacity builder with value 100 (fully opaque).
    /// </summary>
    public static LinkOpacityBuilder V100 => new(100);
}






