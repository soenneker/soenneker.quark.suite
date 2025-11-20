using Soenneker.Quark.Enums;


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
    /// <summary>
    /// Gets a font style builder with inherit keyword.
    /// </summary>
    public static FontStyleBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a font style builder with initial keyword.
    /// </summary>
    public static FontStyleBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a font style builder with revert keyword.
    /// </summary>
    public static FontStyleBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a font style builder with revert-layer keyword.
    /// </summary>
    public static FontStyleBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a font style builder with unset keyword.
    /// </summary>
    public static FontStyleBuilder Unset => new(GlobalKeyword.UnsetValue);
}
