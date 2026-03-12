namespace Soenneker.Quark;

public static class Divide
{
    public static DivideBuilder X => new("divide-x");
    public static DivideBuilder Y => new("divide-y");
    public static DivideBuilder Color(string value) => new("divide", value);
    public static DivideBuilder Opacity(int value) => new("divide-opacity", value.ToString());
}
