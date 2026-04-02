namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating font weight builders with predefined values.
/// </summary>
public static class FontWeight
{
    /// <summary>
    /// Gets a font weight builder with extralight value (200).
    /// </summary>
    public static FontWeightBuilder Extralight => new("extralight");
    /// <summary>
    /// Gets a font weight builder with light value (300).
    /// </summary>
    public static FontWeightBuilder Light => new(FontWeightKeyword.LightValue);
    /// <summary>
    /// Gets a font weight builder with normal value (400).
    /// </summary>
    public static FontWeightBuilder Normal => new(FontWeightKeyword.NormalValue);
    /// <summary>
    /// Gets a font weight builder with medium value (500).
    /// </summary>
    public static FontWeightBuilder Medium => new("medium");
    /// <summary>
    /// Gets a font weight builder with semibold value (600).
    /// </summary>
    public static FontWeightBuilder Semibold => new(FontWeightKeyword.SemiboldValue);
    /// <summary>
    /// Gets a font weight builder with bold value (700).
    /// </summary>
    public static FontWeightBuilder Bold => new(FontWeightKeyword.BoldValue);
    /// <summary>
    /// Gets a font weight builder with extrabold value (800).
    /// </summary>
    public static FontWeightBuilder Extrabold => new("extrabold");
}
