
namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating Z-index builders with predefined values.
/// </summary>
public static class ZIndex
{
    /// <summary>
    /// Gets a Z-index builder with value -1.
    /// </summary>
    public static ZIndexBuilder N1 => new(-1);

    /// <summary>
    /// Gets a Z-index builder with value 0.
    /// </summary>
    public static ZIndexBuilder Z0 => new(0);

    /// <summary>
    /// Gets a Z-index builder with value 1.
    /// </summary>
    public static ZIndexBuilder Z1 => new(1);

    /// <summary>
    /// Gets a Z-index builder with value 2.
    /// </summary>
    public static ZIndexBuilder Z2 => new(2);

    /// <summary>
    /// Gets a Z-index builder with value 3.
    /// </summary>
    public static ZIndexBuilder Z3 => new(3);
}
