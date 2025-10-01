using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class VerticalAlign
{
    public static VerticalAlignBuilder Baseline => new("baseline");
    public static VerticalAlignBuilder Top => new("top");
    public static VerticalAlignBuilder Middle => new("middle");
    public static VerticalAlignBuilder Bottom => new("bottom");
    public static VerticalAlignBuilder TextTop => new("text-top");
    public static VerticalAlignBuilder TextBottom => new("text-bottom");

    public static VerticalAlignBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static VerticalAlignBuilder Initial => new(GlobalKeyword.InitialValue);
    public static VerticalAlignBuilder Revert => new(GlobalKeyword.RevertValue);
    public static VerticalAlignBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static VerticalAlignBuilder Unset => new(GlobalKeyword.UnsetValue);
}
