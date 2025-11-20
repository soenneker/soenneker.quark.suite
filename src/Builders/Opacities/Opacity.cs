
namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating opacity builders with predefined values.
/// </summary>
public static class Opacity
{
    /// <summary>
    /// Gets an opacity builder with value 0 (fully transparent).
    /// </summary>
    public static OpacityBuilder Is0 => new(0);
    /// <summary>
    /// Gets an opacity builder with value 25 (25% opacity).
    /// </summary>
    public static OpacityBuilder Is25 => new(25);
    /// <summary>
    /// Gets an opacity builder with value 50 (50% opacity).
    /// </summary>
    public static OpacityBuilder Is50 => new(50);
    /// <summary>
    /// Gets an opacity builder with value 75 (75% opacity).
    /// </summary>
    public static OpacityBuilder Is75 => new(75);
    /// <summary>
    /// Gets an opacity builder with value 100 (fully opaque).
    /// </summary>
    public static OpacityBuilder Is100 => new(100);
}
