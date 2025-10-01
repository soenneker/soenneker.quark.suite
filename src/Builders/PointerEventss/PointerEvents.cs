using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class PointerEvents
{
    public static PointerEventsBuilder None => new("none");
    public static PointerEventsBuilder Auto => new("auto");

    public static PointerEventsBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static PointerEventsBuilder Initial => new(GlobalKeyword.InitialValue);
    public static PointerEventsBuilder Revert => new(GlobalKeyword.RevertValue);
    public static PointerEventsBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static PointerEventsBuilder Unset => new(GlobalKeyword.UnsetValue);
}
