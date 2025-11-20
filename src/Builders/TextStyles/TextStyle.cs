namespace Soenneker.Quark;

/// <summary>
/// TextStyle utility with fluent API and Bootstrap-first approach.
/// Note: Text alignment is handled by the separate TextAlignment property.
/// </summary>
public static class TextStyle
{
    // Wrapping
    /// <summary>
    /// Gets a text style builder with wrap value (text wraps to multiple lines).
    /// </summary>
    public static TextStyleBuilder Wrap => new("wrap");
    /// <summary>
    /// Gets a text style builder with nowrap value (text does not wrap).
    /// </summary>
    public static TextStyleBuilder Nowrap => new("nowrap");
    /// <summary>
    /// Gets a text style builder with truncate value (text is truncated with ellipsis).
    /// </summary>
    public static TextStyleBuilder Truncate => new("truncate");

    // Transformation
    /// <summary>
    /// Gets a text style builder with lowercase value (text is lowercase).
    /// </summary>
    public static TextStyleBuilder Lowercase => new("lowercase");
    /// <summary>
    /// Gets a text style builder with uppercase value (text is uppercase).
    /// </summary>
    public static TextStyleBuilder Uppercase => new("uppercase");
    /// <summary>
    /// Gets a text style builder with capitalize value (first letter of each word is capitalized).
    /// </summary>
    public static TextStyleBuilder Capitalize => new("capitalize");

    // Styling
    /// <summary>
    /// Gets a text style builder with reset value (resets text styling).
    /// </summary>
    public static TextStyleBuilder Reset => new("reset");
    /// <summary>
    /// Gets a text style builder with muted value (muted text color).
    /// </summary>
    public static TextStyleBuilder Muted => new("muted");
    /// <summary>
    /// Gets a text style builder with emphasis value (emphasized text styling).
    /// </summary>
    public static TextStyleBuilder Emphasis => new("emphasis");
}
