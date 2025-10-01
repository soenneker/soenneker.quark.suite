using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class TextWrap
{
    public static TextWrapBuilder Wrap => new(TextWrapKeyword.WrapValue);
    public static TextWrapBuilder NoWrap => new(TextWrapKeyword.NoWrapValue);

    public static TextWrapBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static TextWrapBuilder Initial => new(GlobalKeyword.InitialValue);
    public static TextWrapBuilder Revert => new(GlobalKeyword.RevertValue);
    public static TextWrapBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static TextWrapBuilder Unset => new(GlobalKeyword.UnsetValue);
}
