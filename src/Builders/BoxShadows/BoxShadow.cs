using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.BoxShadows;

public static class BoxShadow
{
    public static BoxShadowBuilder None => new(BoxShadowKeyword.NoneValue);
    public static BoxShadowBuilder Base => new("base");
    public static BoxShadowBuilder Small => new(SizeType.Small.Value);
    public static BoxShadowBuilder Large => new(SizeType.Large.Value);
}
