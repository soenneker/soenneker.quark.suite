namespace Soenneker.Quark;

public static class Space
{
    public static SpaceBuilder X(int value) => new("space-x", value.ToString());
    public static SpaceBuilder Y(int value) => new("space-y", value.ToString());
    public static SpaceBuilder XReverse => new("space-x-reverse");
    public static SpaceBuilder YReverse => new("space-y-reverse");
}
