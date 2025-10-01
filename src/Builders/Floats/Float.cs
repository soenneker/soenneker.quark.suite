using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class Float
{
    public static FloatBuilder None => new(FloatKeyword.NoneValue);

    public static FloatBuilder Left => new(FloatKeyword.LeftValue);

    public static FloatBuilder Start => new(FloatKeyword.InlineStartValue);

    public static FloatBuilder Right => new(FloatKeyword.RightValue);

    public static FloatBuilder End => new(FloatKeyword.InlineEndValue);

    public static FloatBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static FloatBuilder Initial => new(GlobalKeyword.InitialValue);
    public static FloatBuilder Revert => new(GlobalKeyword.RevertValue);
    public static FloatBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static FloatBuilder Unset => new(GlobalKeyword.UnsetValue);
}
