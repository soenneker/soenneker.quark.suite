namespace Soenneker.Quark;

/// <summary>
/// Static utility for outline style. Tailwind: outline-none, outline, outline-dashed, outline-dotted, outline-double.
/// </summary>
public static class OutlineStyle
{
    public static OutlineStyleBuilder None => new("none");
    public static OutlineStyleBuilder Solid => new("solid");
    public static OutlineStyleBuilder Dashed => new("dashed");
    public static OutlineStyleBuilder Dotted => new("dotted");
    public static OutlineStyleBuilder Double => new("double");
}
