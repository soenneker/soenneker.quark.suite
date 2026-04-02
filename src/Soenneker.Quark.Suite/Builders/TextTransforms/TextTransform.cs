using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text transform builders with predefined values.
/// </summary>
public static class TextTransform
{
    /// <summary>
    /// Gets a text transform builder with lowercase value (text is lowercase).
    /// </summary>
    public static TextTransformBuilder Lowercase => new(TextTransformKeyword.LowercaseValue);
    /// <summary>
    /// Gets a text transform builder with uppercase value (text is uppercase).
    /// </summary>
    public static TextTransformBuilder Uppercase => new(TextTransformKeyword.UppercaseValue);
    /// <summary>
    /// Gets a text transform builder with capitalize value (first letter of each word is capitalized).
    /// </summary>
    public static TextTransformBuilder Capitalize => new(TextTransformKeyword.CapitalizeValue);

    /// <summary>
    /// Gets a text transform builder with inherit keyword.
    /// </summary>
    public static TextTransformBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a text transform builder with initial keyword.
    /// </summary>
    public static TextTransformBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a text transform builder with revert keyword.
    /// </summary>
    public static TextTransformBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a text transform builder with revert-layer keyword.
    /// </summary>
    public static TextTransformBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a text transform builder with unset keyword.
    /// </summary>
    public static TextTransformBuilder Unset => new(GlobalKeyword.UnsetValue);
}
