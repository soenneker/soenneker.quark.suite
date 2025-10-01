using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Simplified text size utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class TextSize
{
    /// <summary>
    /// Extra small text size.
    /// </summary>
    public static TextSizeBuilder ExtraSmall => new(SizeType.ExtraSmall.Value);

    /// <summary>
    /// Small text size.
    /// </summary>
    public static TextSizeBuilder Small => new(SizeType.Small.Value);

    /// <summary>
    /// Base text size (default).
    /// </summary>
    public static TextSizeBuilder Base => new("base");

    /// <summary>
    /// Large text size.
    /// </summary>
    public static TextSizeBuilder Large => new(SizeType.Large.Value);

    /// <summary>
    /// Extra large text size.
    /// </summary>
    public static TextSizeBuilder ExtraLarge => new(SizeType.ExtraLarge.Value);
}
