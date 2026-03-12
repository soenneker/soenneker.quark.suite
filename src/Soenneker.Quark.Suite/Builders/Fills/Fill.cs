namespace Soenneker.Quark;

public static class Fill
{
    public static FillBuilder Token(string value) => new("fill", value);
    public static FillBuilder None => new("fill", "none");
    public static FillBuilder Current => new("fill", "current");
}
