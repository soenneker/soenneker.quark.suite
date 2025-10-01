using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class TextDecoration
{
    public static TextDecorationBuilder None => new("none");
    public static TextDecorationBuilder Underline => new("underline");
    public static TextDecorationBuilder LineThrough => new("line-through");

    public static TextDecorationBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static TextDecorationBuilder Initial => new(GlobalKeyword.InitialValue);
    public static TextDecorationBuilder Revert => new(GlobalKeyword.RevertValue);
    public static TextDecorationBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static TextDecorationBuilder Unset => new(GlobalKeyword.UnsetValue);
}
