using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Visibilities;

public static class Visibility
{
    public static VisibilityBuilder Visible => new(VisibilityKeyword.VisibleValue);

    public static VisibilityBuilder Invisible => new("invisible");

    public static VisibilityBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static VisibilityBuilder Initial => new(GlobalKeyword.InitialValue);
    public static VisibilityBuilder Revert => new(GlobalKeyword.RevertValue);
    public static VisibilityBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static VisibilityBuilder Unset => new(GlobalKeyword.UnsetValue);
}
