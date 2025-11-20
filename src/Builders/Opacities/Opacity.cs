
namespace Soenneker.Quark;

public static class Opacity
{
    public static OpacityBuilder Is0 => new(0);
    public static OpacityBuilder Is25 => new(25);
    public static OpacityBuilder Is50 => new(50);
    public static OpacityBuilder Is75 => new(75);
    public static OpacityBuilder Is100 => new(100);
}
