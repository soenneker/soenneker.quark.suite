namespace Soenneker.Quark;

/// <summary>
/// Simplified text overflow utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class TextOverflow
{
    /// <summary>
    /// Clips the overflowing text at the content area boundary.
    /// </summary>
    public static TextOverflowBuilder Clip => new(TextOverflowKeyword.Clip);

    /// <summary>
    /// Displays an ellipsis ('…') to represent clipped text.
    /// </summary>
    public static TextOverflowBuilder Ellipsis => new(TextOverflowKeyword.Ellipsis);
}
