using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class FontWeight
{
    // Note: Lighter and Bolder are not available in the current FontWeightKeyword package version
    // public static FontWeightBuilder Lighter => new(FontWeightKeyword.LighterValue);
    public static FontWeightBuilder Light => new(FontWeightKeyword.LightValue);
    public static FontWeightBuilder Normal => new(FontWeightKeyword.NormalValue);
    public static FontWeightBuilder Medium => new(FontWeightKeyword.MediumValue);
    public static FontWeightBuilder Semibold => new(FontWeightKeyword.SemiboldValue);
    public static FontWeightBuilder Bold => new(FontWeightKeyword.BoldValue);
    // public static FontWeightBuilder Bolder => new(FontWeightKeyword.BolderValue);
    public static FontWeightBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static FontWeightBuilder Initial => new(GlobalKeyword.InitialValue);
    public static FontWeightBuilder Revert => new(GlobalKeyword.RevertValue);
    public static FontWeightBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static FontWeightBuilder Unset => new(GlobalKeyword.UnsetValue);
}
