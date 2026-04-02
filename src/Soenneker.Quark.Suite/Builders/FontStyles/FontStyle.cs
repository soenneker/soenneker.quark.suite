namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating font style builders with predefined values.
/// </summary>
public static class FontStyle
{
    /// <summary>
    /// Gets a font style builder with italic value (text is italic).
    /// </summary>
    public static FontStyleBuilder Italic => new(FontStyleKeyword.ItalicValue);
    /// <summary>
    /// Gets a font style builder with normal value (text is not italic).
    /// </summary>
    public static FontStyleBuilder Normal => new(FontStyleKeyword.NormalValue);
}
