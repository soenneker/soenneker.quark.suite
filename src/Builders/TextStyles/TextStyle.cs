namespace Soenneker.Quark;

/// <summary>
/// TextStyle utility with fluent API and Bootstrap-first approach.
/// Note: Text alignment is handled by the separate TextAlignment property.
/// </summary>
public static class TextStyle
{
    // Wrapping
    public static TextStyleBuilder Wrap => new("wrap");
    public static TextStyleBuilder Nowrap => new("nowrap");
    public static TextStyleBuilder Truncate => new("truncate");

    // Transformation
    public static TextStyleBuilder Lowercase => new("lowercase");
    public static TextStyleBuilder Uppercase => new("uppercase");
    public static TextStyleBuilder Capitalize => new("capitalize");

    // Styling
    public static TextStyleBuilder Reset => new("reset");
    public static TextStyleBuilder Muted => new("muted");
    public static TextStyleBuilder Emphasis => new("emphasis");
}
