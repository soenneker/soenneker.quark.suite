namespace Soenneker.Quark;

/// <summary>
/// TextStyle utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class TextStyle
{
    // Alignment
    public static TextStyleBuilder Start => new("start");
    public static TextStyleBuilder Center => new("center");
    public static TextStyleBuilder End => new("end");
    public static TextStyleBuilder Justify => new("justify");

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
