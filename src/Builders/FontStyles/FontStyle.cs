using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

public static class FontStyle
{
    public static FontStyleBuilder Italic => new(FontStyleKeyword.ItalicValue);
    public static FontStyleBuilder Normal => new(FontStyleKeyword.NormalValue);
    public static FontStyleBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static FontStyleBuilder Initial => new(GlobalKeyword.InitialValue);
    public static FontStyleBuilder Revert => new(GlobalKeyword.RevertValue);
    public static FontStyleBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static FontStyleBuilder Unset => new(GlobalKeyword.UnsetValue);
}
