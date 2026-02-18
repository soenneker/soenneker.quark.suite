namespace Soenneker.Quark;

public static class Gradient
{
    public static GradientBuilder To(string value) => new("bg-gradient-to", value);
    public static GradientBuilder From(string value) => new("from", value);
    public static GradientBuilder Via(string value) => new("via", value);
    public static GradientBuilder End(string value) => new("to", value);
}
