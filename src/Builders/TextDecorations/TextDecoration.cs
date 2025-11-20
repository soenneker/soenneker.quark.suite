using Soenneker.Quark.Enums;


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
    /// Gets a text decoration builder with inherit keyword.
    /// </summary>
    public static TextDecorationBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a text decoration builder with initial keyword.
    /// </summary>
    public static TextDecorationBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a text decoration builder with revert keyword.
    /// </summary>
    public static TextDecorationBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a text decoration builder with revert-layer keyword.
    /// </summary>
    public static TextDecorationBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a text decoration builder with unset keyword.
    /// </summary>
    public static TextDecorationBuilder Unset => new(GlobalKeyword.UnsetValue);
}
