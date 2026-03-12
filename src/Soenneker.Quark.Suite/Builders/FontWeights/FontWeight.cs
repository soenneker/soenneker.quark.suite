using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating font weight builders with predefined values.
/// </summary>
public static class FontWeight
{
    /// <summary>
    /// Gets a font weight builder with lighter value (thinner than parent).
    /// </summary>
    public static FontWeightBuilder Lighter => new(FontWeightKeyword.LighterValue);
    /// <summary>
    /// Gets a font weight builder with light value (300).
    /// </summary>
    public static FontWeightBuilder Light => new(FontWeightKeyword.LightValue);
    /// <summary>
    /// Gets a font weight builder with normal value (400).
    /// </summary>
    public static FontWeightBuilder Normal => new(FontWeightKeyword.NormalValue);
    /// <summary>
    /// Gets a font weight builder with semibold value (600).
    /// </summary>
    public static FontWeightBuilder Semibold => new(FontWeightKeyword.SemiboldValue);
    /// <summary>
    /// Gets a font weight builder with bold value (700).
    /// </summary>
    public static FontWeightBuilder Bold => new(FontWeightKeyword.BoldValue);
    /// <summary>
    /// Gets a font weight builder with bolder value (thicker than parent).
    /// </summary>
    public static FontWeightBuilder Bolder => new(FontWeightKeyword.BolderValue);
    /// <summary>
    /// Gets a font weight builder with inherit keyword.
    /// </summary>
    public static FontWeightBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a font weight builder with initial keyword.
    /// </summary>
    public static FontWeightBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a font weight builder with revert keyword.
    /// </summary>
    public static FontWeightBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a font weight builder with revert-layer keyword.
    /// </summary>
    public static FontWeightBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a font weight builder with unset keyword.
    /// </summary>
    public static FontWeightBuilder Unset => new(GlobalKeyword.UnsetValue);
}
