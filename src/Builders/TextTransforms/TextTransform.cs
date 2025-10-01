using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class TextTransform
{
    public static TextTransformBuilder Lowercase => new(TextTransformKeyword.LowercaseValue);
    public static TextTransformBuilder Uppercase => new(TextTransformKeyword.UppercaseValue);
    public static TextTransformBuilder Capitalize => new(TextTransformKeyword.CapitalizeValue);

    public static TextTransformBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static TextTransformBuilder Initial => new(GlobalKeyword.InitialValue);
    public static TextTransformBuilder Revert => new(GlobalKeyword.RevertValue);
    public static TextTransformBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static TextTransformBuilder Unset => new(GlobalKeyword.UnsetValue);
}
