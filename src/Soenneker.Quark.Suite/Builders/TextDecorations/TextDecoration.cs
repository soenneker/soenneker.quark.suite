namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text decoration builders with predefined values.
/// </summary>
public static class TextDecoration
{
    /// <summary>
    /// Gets a text decoration builder with none value (no decoration).
    /// </summary>
    public static TextDecorationBuilder None => new("none");
    /// <summary>
    /// Gets a text decoration builder with underline value (text is underlined).
    /// </summary>
    public static TextDecorationBuilder Underline => new("underline");
    /// <summary>
    /// Gets a text decoration builder with line-through value (text has a line through it).
    /// </summary>
    public static TextDecorationBuilder LineThrough => new("line-through");
    /// <summary>
    /// Gets a text decoration builder with overline value.
    /// </summary>
    public static TextDecorationBuilder Overline => new("overline");
}
