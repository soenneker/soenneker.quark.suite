using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating box shadow builders with predefined values.
/// </summary>
public static class BoxShadow
{
    /// <summary>
    /// Gets a box shadow builder with none value (no shadow).
    /// </summary>
    public static BoxShadowBuilder None => new(BoxShadowKeyword.NoneValue);
    /// <summary>
    /// Gets a box shadow builder with base value (default shadow).
    /// </summary>
    public static BoxShadowBuilder Base => new("base");
    /// <summary>
    /// Gets a box shadow builder with small value (small shadow).
    /// </summary>
    public static BoxShadowBuilder Small => new(SizeType.Small.Value);
    /// <summary>
    /// Gets a box shadow builder with large value (large shadow).
    /// </summary>
    public static BoxShadowBuilder Large => new(SizeType.Large.Value);
}
