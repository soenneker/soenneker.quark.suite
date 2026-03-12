using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating line height builders with predefined values.
/// </summary>
public static class LineHeight
{
    /// <summary>
    /// Gets a line height builder with value 1 (tight line height).
    /// </summary>
    public static LineHeightBuilder Is1 => new(ScaleType.Is1.Value);
    /// <summary>
    /// Gets a line height builder with small value.
    /// </summary>
    public static LineHeightBuilder Small => new(SizeType.Small.Value);
    /// <summary>
    /// Gets a line height builder with base value (default line height).
    /// </summary>
    public static LineHeightBuilder Base => new("base");
    /// <summary>
    /// Gets a line height builder with large value.
    /// </summary>
    public static LineHeightBuilder Large => new(SizeType.Large.Value);
}
