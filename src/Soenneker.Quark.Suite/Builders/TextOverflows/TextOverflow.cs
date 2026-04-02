using Soenneker.Quark.Enums;


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

    /// <summary>
    /// The element inherits the text-overflow value from its parent element.
    /// </summary>
    public static TextOverflowBuilder Inherit => new(GlobalKeyword.Inherit);

    /// <summary>
    /// The element resets the text-overflow property to its initial value.
    /// </summary>
    public static TextOverflowBuilder Initial => new(GlobalKeyword.Initial);

    /// <summary>
    /// The element resets the text-overflow property to the value established by the user agent's default stylesheet.
    /// </summary>
    public static TextOverflowBuilder Revert => new(GlobalKeyword.Revert);

    /// <summary>
    /// The element resets the text-overflow property to the value established by the user agent's default stylesheet for the current element.
    /// </summary>
    public static TextOverflowBuilder RevertLayer => new(GlobalKeyword.RevertLayer);

    /// <summary>
    /// The element resets the text-overflow property to its inherited value if it inherits, or to its initial value if it doesn't.
    /// </summary>
    public static TextOverflowBuilder Unset => new(GlobalKeyword.Unset);
}
